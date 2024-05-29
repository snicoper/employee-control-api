﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using EmployeeControl.Application.Common.Constants;
using EmployeeControl.Application.Common.Interfaces.Users;
using EmployeeControl.Application.Common.Models.Settings;
using EmployeeControl.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeControl.Infrastructure.Services.Users;

public class TokenService(IOptions<JwtSettings> jwtSettings, UserManager<User> userManager)
    : ITokenService
{
    public async Task<string> GenerateAccessTokenAsync(User user)
    {
        // Añadir claims.
        var claims = new List<Claim>
        {
            new(ClaimTypes.Sid, user.Id),
            new(ClaimTypes.Name, user.UserName ?? string.Empty),
            new(CustomClaims.CompanyId, user.CompanyId)
        };

        var roles = await userManager.GetRolesAsync(user);

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Value.Key ?? string.Empty));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new JwtSecurityToken(
            jwtSettings.Value.Issuer,
            jwtSettings.Value.Audience,
            claims,
            expires: DateTime.Now.AddMinutes(jwtSettings.Value.AccessTokenLifeTimeMinutes),
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
