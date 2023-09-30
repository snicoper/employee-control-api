﻿using EmployeeControl.Application.Common.Exceptions;
using EmployeeControl.Application.Common.Models.Options;
using EmployeeControl.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeControl.Application.Cqrs.Identity.Commands.Login;

public class LoginHandler : IRequestHandler<LoginCommand, LoginDto>
{
    private readonly JwtOptions _jwtOptionses;
    private readonly UserManager<ApplicationUser> _userManager;

    public LoginHandler(UserManager<ApplicationUser> userManager, IOptions<JwtOptions> jwtOptions)
    {
        _userManager = userManager;
        _jwtOptionses = jwtOptions.Value;
    }

    public async Task<LoginDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        // Verifica credenciales con Identity y obtniene las Claims.
        var user = await CheckPasswordAndGetUserAsync(request);
        var roles = await _userManager.GetRolesAsync(user);
        var claims = GetUserClaims(user, roles);

        // Genera un token según los claims.
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptionses.Key ?? string.Empty));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new JwtSecurityToken(
            _jwtOptionses.Issuer,
            _jwtOptionses.Audience,
            claims,
            expires: DateTime.Now.AddDays(_jwtOptionses.LifeTimeDays),
            signingCredentials: credentials);

        var jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

        return new LoginDto { Token = jwt };
    }

    private async Task<ApplicationUser> CheckPasswordAndGetUserAsync(LoginCommand request)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);

        if (user is null || !await _userManager.CheckPasswordAsync(user, request.Password))
        {
            throw new ForbiddenAccessException();
        }

        return user;
    }

    private IEnumerable<Claim> GetUserClaims(ApplicationUser user, IEnumerable<string> roles)
    {
        var userName = user.UserName ?? string.Empty;
        var claims = new List<Claim> { new(ClaimTypes.Sid, user.Id), new(ClaimTypes.Name, userName) };
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        return claims;
    }
}
