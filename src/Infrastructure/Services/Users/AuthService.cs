using EmployeeControl.Application.Common.Constants;
using EmployeeControl.Application.Common.Extensions;
using EmployeeControl.Application.Common.Interfaces.Common;
using EmployeeControl.Application.Common.Interfaces.Users;
using EmployeeControl.Application.Common.Localization;
using EmployeeControl.Application.Common.Models.Settings;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace EmployeeControl.Infrastructure.Services.Users;

public class AuthService(
    UserManager<User> userManager,
    IOptions<JwtSettings> jwtSettings,
    ITokenService tokenService,
    IDateTimeProvider dateTimeProvider,
    IStringLocalizer<IdentityResource> localizer)
    : IAuthService
{
    public async Task<(string AccessToken, string RefreshToken)> LoginAsync(string email, string password)
    {
        var user = userManager.Users.SingleOrDefault(au => au.Email == email);

        // Validación de usuario y password, 401 si no es valido el login.
        if (user is null || !await userManager.CheckPasswordAsync(user, password))
        {
            throw new UnauthorizedAccessException();
        }

        // Validación email confirmado.
        if (!user.EmailConfirmed)
        {
            var errorMessage = localizer["El correo esta pendiente de validación desde tu bandeja de correo."];

            Result
                .Failure(ValidationErrorsKeys.NonFieldErrors, errorMessage)
                .RaiseBadRequestIfErrorsExist();
        }

        // Validación cuenta activa.
        if (!user.Active)
        {
            var errorMessage = localizer["La cuenta no esta activa, debes hablar con un responsable de tu empresa."];

            Result
                .Failure(ValidationErrorsKeys.NotificationErrors, errorMessage)
                .RaiseBadRequestIfErrorsExist();
        }

        var tokensResult = await GenerateUserTokenAsync(user);

        return tokensResult;
    }

    public async Task<(string AccessToken, string RefreshToken)> RefreshTokenAsync(string refreshToken)
    {
        var user = userManager.Users.SingleOrDefault(au => au.RefreshToken == refreshToken);

        if (user is null || !user.Active || user.RefreshTokenExpiryTime <= dateTimeProvider.UtcNow)
        {
            throw new UnauthorizedAccessException();
        }

        var tokensResult = await GenerateUserTokenAsync(user);

        return tokensResult;
    }

    private async Task<(string AccessToken, string RefreshToken)> GenerateUserTokenAsync(User user)
    {
        var jwt = await tokenService.GenerateAccessTokenAsync(user);
        var newRefreshToken = tokenService.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = dateTimeProvider.UtcNow.AddDays(jwtSettings.Value.RefreshTokenLifeTimeDays);

        await userManager.UpdateAsync(user);

        return (jwt, newRefreshToken);
    }
}
