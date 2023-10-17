using EmployeeControl.Application.Common.Extensions;
using EmployeeControl.Application.Common.Interfaces.Entities.Identity;
using EmployeeControl.Application.Common.Models.Settings;
using EmployeeControl.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EmployeeControl.Infrastructure.Services.Entities.Identity;

public class TokenService(IOptions<JwtSettings> jwtOptions, UserManager<ApplicationUser> userManager)
    : ITokenService
{
    private readonly JwtSettings _jwtSettings = jwtOptions.Value;

    public async Task<string> GenerateAccessTokenAsync(ApplicationUser user)
    {
        var claims = new List<Claim> { new(ClaimTypes.Sid, user.Id), new(ClaimTypes.Name, user.UserName.SetEmptyIfNull()) };
        var roles = await userManager.GetRolesAsync(user);

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key.SetEmptyIfNull()));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new JwtSecurityToken(
            _jwtSettings.Issuer,
            _jwtSettings.Audience,
            claims,
            expires: DateTime.Now.AddMinutes(_jwtSettings.AccessTokenLifeTimeMinutes),
            signingCredentials: credentials);

        var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

        return token;
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var randomNumberGenerator = RandomNumberGenerator.Create();
        randomNumberGenerator.GetBytes(randomNumber);
        var tokenRefresh = Convert.ToBase64String(randomNumber);

        return tokenRefresh;
    }
}
