using EmployeeControl.Application.Common.Interfaces.Common;
using EmployeeControl.Application.Common.Interfaces.Entities.Identity;
using EmployeeControl.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace EmployeeControl.Infrastructure.Services.Entities.Identity;

public class IdentityValidatorService(
        UserManager<ApplicationUser> userManager,
        IUserValidator<ApplicationUser> userValidator,
        IPasswordValidator<ApplicationUser> passwordValidator,
        IStringLocalizer<ApplicationUser> localizer,
        IValidationFailureService validationFailureService,
        ILogger<IdentityService> logger)
    : IIdentityValidatorService
{
    public async Task UniqueEmailValidationAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        var isRegistered = await userManager.Users.AnyAsync(au => au.Email == user.Email, cancellationToken);

        if (isRegistered)
        {
            var errorMessage = localizer["El email ya esta registrado."];
            logger.LogWarning("{message}", errorMessage);
            validationFailureService.Add(nameof(user.Email), errorMessage);
        }
    }

    public async Task UserValidationAsync(ApplicationUser user)
    {
        var validUser = await userValidator.ValidateAsync(userManager, user);
        if (!validUser.Succeeded)
        {
            var errorMessage = localizer["El usuario no es valido."];
            logger.LogWarning("{message}", errorMessage);
            validationFailureService.Add(nameof(user.UserName), errorMessage);
        }
    }

    public async Task PasswordValidationAsync(ApplicationUser user, string password)
    {
        var validPassword = await passwordValidator.ValidateAsync(userManager, user, password);
        if (!validPassword.Succeeded)
        {
            var errorMessage = localizer["La contraseña no es valida."];
            logger.LogWarning("{message}", errorMessage);
            validationFailureService.Add("Password", errorMessage);
        }
    }

    public async Task CheckPasswordValidationAsync(ApplicationUser user, string password)
    {
        if (!await userManager.CheckPasswordAsync(user, password))
        {
            var errorMessage = localizer["La contraseña no parece valida."];
            logger.LogWarning("{message}", errorMessage);
            validationFailureService.Add("Password", errorMessage);
        }
    }
}
