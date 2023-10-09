using EmployeeControl.Application.Common.Interfaces;
using EmployeeControl.Application.Common.Interfaces.Identity;
using EmployeeControl.Application.Common.Models.Settings;
using EmployeeControl.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace EmployeeControl.Application.Common.Services.Identity;

public class AuthService : IAuthService
{
    private readonly JwtSettings _jwtSettings;
    private readonly ITokenService _tokenService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IDateTime _dateTime;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        IOptions<JwtSettings> options,
        IDateTime dateTime,
        ITokenService tokenService)
    {
        _userManager = userManager;
        _dateTime = dateTime;
        _jwtSettings = options.Value;
        _tokenService = tokenService;
    }

    public async Task<(string AccessToken, string RefreshToken)> LoginAsync(string identifier, string password)
    {
        var user = _userManager.Users.SingleOrDefault(au => au.UserName == identifier || au.Email == identifier);

        if (user is null || !await _userManager.CheckPasswordAsync(user, password))
        {
            throw new UnauthorizedAccessException();
        }

        return await GenerateUserTokenAsync(user);
    }

    public async Task<(string AccessToken, string RefreshToken)> RefreshTokenAsync(string refreshToken)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.RefreshToken == refreshToken);

        if (user is null || user.RefreshTokenExpiryTime <= _dateTime.UtcNow)
        {
            throw new UnauthorizedAccessException();
        }

        return await GenerateUserTokenAsync(user);
    }

    private async Task<(string AccessToken, string RefreshToken)> GenerateUserTokenAsync(ApplicationUser user)
    {
        var jwt = await _tokenService.GenerateAccessTokenAsync(user);
        var newRefreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = _dateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenLifeTimeDays);

        await _userManager.UpdateAsync(user);

        return (jwt, newRefreshToken);
    }
}
