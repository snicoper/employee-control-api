using EmployeeControl.Application.Common.Constants;
using EmployeeControl.Application.Common.Interfaces.Users;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Entities;
using EmployeeControl.Domain.Validators;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace EmployeeControl.Infrastructure.Validators;

public class IdentityValidator(
    UserManager<User> userManager,
    IUserValidator<User> userValidator,
    IPasswordValidator<User> passwordValidator,
    IStringLocalizer<User> localizer,
    ICurrentUserService currentUserService,
    ILogger<IdentityValidator> logger)
    : IIdentityValidator
{
    public async Task<Result> UniqueEmailValidationAsync(User user, Result result, CancellationToken cancellationToken)
    {
        // Si es un update, omitir el email actual del usuario.
        var emailExists = string.IsNullOrEmpty(user.Id)
            ? await userManager.Users.AnyAsync(au => au.Email == user.Email, cancellationToken)
            : await userManager.Users.AnyAsync(au => au.Email == user.Email && au.Id != user.Id, cancellationToken);

        if (!emailExists)
        {
            return result;
        }

        var errorMessage = localizer["El email ya esta registrado."];
        logger.LogError(errorMessage);
        result.AddError(nameof(user.Email), errorMessage);

        return result;
    }

    public async Task<Result> UserValidationAsync(User user, Result result)
    {
        var validUser = await userValidator.ValidateAsync(userManager, user);

        if (validUser.Succeeded)
        {
            return result;
        }

        var errorMessage = localizer["El usuario no es valido."];
        logger.LogWarning(errorMessage);
        result.AddError(nameof(user.UserName), errorMessage);

        return result;
    }

    public async Task<Result> PasswordValidationAsync(User user, string password, Result result)
    {
        var validPassword = await passwordValidator.ValidateAsync(userManager, user, password);
        if (!validPassword.Succeeded)
        {
            var errorMessage = localizer["La contraseña no es valida."];
            logger.LogWarning(errorMessage);
            result.AddError("Password", errorMessage);
        }

        return result;
    }

    public Result ValidateUpdateEmployeeRoles(User user, IEnumerable<string> userRoles, Result result)
    {
        if (currentUserService.Id != user.Id)
        {
            return result;
        }

        var errorMessage = localizer["Un usuario no puede editar sus propios Roles."];
        logger.LogWarning(errorMessage);
        result.AddError(ValidationErrorsKeys.NotificationErrors, errorMessage);

        return result;
    }
}
