using EmployeeControl.Application.Common.Interfaces;
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
    public async Task UniqueEmailValidationAsync(ApplicationUser applicationUser, CancellationToken cancellationToken)
    {
        var isRegistered = await userManager.Users.AnyAsync(au => au.Email == applicationUser.Email, cancellationToken);

        if (isRegistered)
        {
            var errorMessage = localizer["El email ya esta registrado."];
            logger.LogWarning("{message}", errorMessage);
            validationFailureService.Add(nameof(applicationUser.Email), errorMessage);
        }
    }

    public async Task UserValidationAsync(ApplicationUser applicationUser)
    {
        var validUser = await userValidator.ValidateAsync(userManager, applicationUser);
        if (!validUser.Succeeded)
        {
            var errorMessage = localizer["El usuario no es valido."];
            logger.LogWarning("{message}", errorMessage);
            validationFailureService.Add(nameof(applicationUser.UserName), errorMessage);
        }
    }

    public async Task PasswordValidationAsync(ApplicationUser applicationUser, string password)
    {
        var validPassword = await passwordValidator.ValidateAsync(userManager, applicationUser, password);
        if (!validPassword.Succeeded)
        {
            var errorMessage = localizer["La contraseña no es valida."];
            logger.LogWarning("{message}", errorMessage);
            validationFailureService.Add("Password", errorMessage);
        }
    }
}
