using EmployeeControl.Application.Common.Interfaces;
using EmployeeControl.Application.Common.Interfaces.Identity;
using EmployeeControl.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace EmployeeControl.Infrastructure.Services.Identity;

public class IdentityCreateValidationService(
        UserManager<ApplicationUser> userManager,
        IUserValidator<ApplicationUser> userValidator,
        IPasswordValidator<ApplicationUser> passwordValidator,
        IStringLocalizer<ApplicationUser> localizer,
        IValidationFailureService validationFailureService,
        ILogger<IdentityService> logger)
    : IIdentityCreateValidationService
{
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
