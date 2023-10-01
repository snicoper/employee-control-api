using EmployeeControl.Application.Common.Extensions;
using EmployeeControl.Application.Common.Interfaces.Identity;
using EmployeeControl.Application.Common.Models.Settings;
using EmployeeControl.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeControl.Application.Common.Services.Identity;

public class LoginService : ILoginService
{
    private readonly JwtSettings _jwtSettings;
    private readonly UserManager<ApplicationUser> _userManager;

    public LoginService(IOptions<JwtSettings> jwtOptions, UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
        _jwtSettings = jwtOptions.Value;
    }

    public async Task<string> LoginAsync(string identifier, string password)
    {
        // Verifica credenciales con Identity y obtiene las Claims.
        var user = await CheckPasswordAndGetUserAsync(identifier, password);
        var roles = await _userManager.GetRolesAsync(user);
        var claims = UserClaims(user, roles);

        // Genera un token según los claims.
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key.NotNull()));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new JwtSecurityToken(
            _jwtSettings.Issuer,
            _jwtSettings.Audience,
            claims,
            expires: DateTime.Now.AddDays(_jwtSettings.LifeTimeDays),
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

    private IEnumerable<Claim> UserClaims(ApplicationUser user, IEnumerable<string> roles)
    {
        var claims = new List<Claim> { new(ClaimTypes.Sid, user.Id), new(ClaimTypes.Name, user.UserName.NotNull()) };

        // Roles in claims.
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        return claims;
    }
}
