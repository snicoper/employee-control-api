using EmployeeControl.Application.Common.Exceptions;
using EmployeeControl.Application.Common.Extensions;
using EmployeeControl.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeControl.Application.CommandAndQueries.Identity.Commands.Login;

public class LoginHandler : IRequestHandler<LoginCommand, LoginDto>
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<ApplicationUser> _userManager;

    public LoginHandler(UserManager<ApplicationUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<LoginDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var jwtKey = _configuration["Jwt:Key"].RaiseConfigurationNullParameterExceptionIfNullOrEmpty();
        var jwtIssuer = _configuration["Jwt:Issuer"].RaiseConfigurationNullParameterExceptionIfNullOrEmpty();
        var jwtAudience = _configuration["Jwt:Audience"].RaiseConfigurationNullParameterExceptionIfNullOrEmpty();

        // La jwtKey debe tener al menos 32 caracteres.
        if (jwtKey.Length < 32)
        {
            throw new ConfigurationBadParameterException("The key must have at least 32 characters");
        }

        // Verifica credenciales con Identity y obtniene las Claims.
        var user = await CheckPasswordAndGetUserAsync(request);
        var roles = await _userManager.GetRolesAsync(user);
        var claims = GetUserClaims(user, roles);

        // Genera un token según los claims.
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new JwtSecurityToken(
            jwtIssuer,
            jwtAudience,
            claims,
            expires: DateTime.Now.AddDays(30),
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
        var userName = user.UserName.RaiseConfigurationNullParameterExceptionIfNullOrEmpty();
        var claims = new List<Claim> { new(ClaimTypes.Sid, user.Id), new(ClaimTypes.Name, userName) };
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        return claims;
    }
}
