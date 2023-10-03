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

    public AuthService(UserManager<ApplicationUser> userManager, IOptions<JwtSettings> options, ITokenService tokenService)
    {
        _userManager = userManager;
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

        var jwt = await _tokenService.GenerateAccessTokenAsync(user);
        var refreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwtSettings.ExpirationTokenLifeTimeDays);

        await _userManager.UpdateAsync(user);

        return (jwt, refreshToken);
    }
}
