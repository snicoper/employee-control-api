using EmployeeControl.Application.Common.Interfaces;
using EmployeeControl.Application.Common.Models.Options;
using EmployeeControl.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeControl.Infrastructure.Services;

public class LoginService : ILoginService
{
    private readonly JwtOptions _jwtOptions;
    private readonly UserManager<ApplicationUser> _userManager;

    public LoginService(IOptions<JwtOptions> jwtOptions, UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
        _jwtOptions = jwtOptions.Value;
    }

    public async Task<string> LoginAsync(string identifier, string password)
    {
        // Verifica credenciales con Identity y obtniene las Claims.
        var user = await CheckPasswordAndGetUserAsync(identifier, password);
        var roles = await _userManager.GetRolesAsync(user);
        var claims = GetUserClaims(user, roles);

        // Genera un token según los claims.
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key ?? string.Empty));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new JwtSecurityToken(
            _jwtOptions.Issuer,
            _jwtOptions.Audience,
            claims,
            expires: DateTime.Now.AddDays(_jwtOptions.LifeTimeDays),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }

    private async Task<ApplicationUser> CheckPasswordAndGetUserAsync(string identifier, string password)
    {
        var user = _userManager.Users.SingleOrDefault(au => au.UserName == identifier || au.Email == identifier);

        if (user is null || !await _userManager.CheckPasswordAsync(user, password))
        {
            throw new UnauthorizedAccessException();
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
