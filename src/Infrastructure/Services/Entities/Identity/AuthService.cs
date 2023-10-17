using EmployeeControl.Application.Common.Constants;
using EmployeeControl.Application.Common.Interfaces;
using EmployeeControl.Application.Common.Interfaces.Entities.Identity;
using EmployeeControl.Application.Common.Models.Settings;
using EmployeeControl.Application.Localizations;
using EmployeeControl.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace EmployeeControl.Infrastructure.Services.Entities.Identity;

public class AuthService(
        UserManager<ApplicationUser> userManager,
        IOptions<JwtSettings> options,
        IDateTimeService dateTimeService,
        ITokenService tokenService,
        IStringLocalizer<IdentityLocalizer> localizer,
        IValidationFailureService validationFailureService)
    : IAuthService
{
    private readonly JwtSettings _jwtSettings = options.Value;

    public async Task<(string AccessToken, string RefreshToken)> LoginAsync(string email, string password)
    {
        var user = userManager.Users.SingleOrDefault(au => au.Email == email);

        // Validación del password.
        if (user is null || !await userManager.CheckPasswordAsync(user, password))
        {
            throw new UnauthorizedAccessException();
        }

        // Validación email confirmado.
        if (!user.EmailConfirmed)
        {
            validationFailureService.AddAndRaiseException(
                ValidationErrorsKeys.NonFieldErrors,
                localizer["El correo ha de ser validado."]);
        }

        // Validación cuenta activa.
        if (!user.Active)
        {
            validationFailureService.AddAndRaiseException(
                ValidationErrorsKeys.NonFieldErrors,
                localizer["La cuenta no esta activa."]);
        }

        var tokensResult = await GenerateUserTokenAsync(user);

        return tokensResult;
    }

    public async Task<(string AccessToken, string RefreshToken)> RefreshTokenAsync(string refreshToken)
    {
        var user = userManager.Users.SingleOrDefault(au => au.RefreshToken == refreshToken);

        if (user is null || user.RefreshTokenExpiryTime <= dateTimeService.UtcNow)
        {
            throw new UnauthorizedAccessException();
        }

        var tokensResult = await GenerateUserTokenAsync(user);

        return tokensResult;
    }

    private async Task<(string AccessToken, string RefreshToken)> GenerateUserTokenAsync(ApplicationUser user)
    {
        var jwt = await tokenService.GenerateAccessTokenAsync(user);
        var newRefreshToken = tokenService.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = dateTimeService.UtcNow.AddDays(_jwtSettings.RefreshTokenLifeTimeDays);

        await userManager.UpdateAsync(user);

        return (jwt, newRefreshToken);
    }
}
