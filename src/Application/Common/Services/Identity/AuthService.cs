using EmployeeControl.Application.Common.Interfaces;
using EmployeeControl.Application.Common.Interfaces.Identity;
using EmployeeControl.Application.Common.Models.Settings;
using EmployeeControl.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace EmployeeControl.Application.Common.Services.Identity;

public class AuthService(
        UserManager<ApplicationUser> userManager,
        IOptions<JwtSettings> options,
        IDateTime dateTime,
        ITokenService tokenService)
    : IAuthService
{
    private readonly JwtSettings _jwtSettings = options.Value;

    public async Task<(string AccessToken, string RefreshToken)> LoginAsync(string identifier, string password)
    {
        var user = userManager.Users.SingleOrDefault(au => au.UserName == identifier || au.Email == identifier);

        if (user is null || !await userManager.CheckPasswordAsync(user, password))
        {
            throw new UnauthorizedAccessException();
        }

        return await GenerateUserTokenAsync(user);
    }

    public async Task<(string AccessToken, string RefreshToken)> RefreshTokenAsync(string refreshToken)
    {
        var user = userManager.Users.SingleOrDefault(u => u.RefreshToken == refreshToken);

        if (user is null || user.RefreshTokenExpiryTime <= dateTime.UtcNow)
        {
            throw new UnauthorizedAccessException();
        }

        return await GenerateUserTokenAsync(user);
    }

    private async Task<(string AccessToken, string RefreshToken)> GenerateUserTokenAsync(ApplicationUser user)
    {
        var jwt = await tokenService.GenerateAccessTokenAsync(user);
        var newRefreshToken = tokenService.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = dateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenLifeTimeDays);

        await userManager.UpdateAsync(user);

        return (jwt, newRefreshToken);
    }
}
