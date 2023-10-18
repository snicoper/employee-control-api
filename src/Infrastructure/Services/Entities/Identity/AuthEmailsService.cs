using EmployeeControl.Application.Common.Constants;
using EmployeeControl.Application.Common.Extensions;
using EmployeeControl.Application.Common.Interfaces;
using EmployeeControl.Application.Common.Interfaces.Entities.Identity;
using EmployeeControl.Application.Common.Models.Emails;
using EmployeeControl.Application.Localizations;
using EmployeeControl.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace EmployeeControl.Infrastructure.Services.Entities.Identity;

public class AuthEmailsService(
        UserManager<ApplicationUser> userManager,
        IEmailService emailService,
        ILinkGeneratorService linkGeneratorService,
        IStringLocalizer<SharedLocalizer> localizer)
    : IAuthEmailsService
{
    public async Task SendValidateEmailAsync(ApplicationUser user, Domain.Entities.Company company)
    {
        // Generar code de validación.
        var code = await userManager.GenerateEmailConfirmationTokenAsync(user);

        // Url validación.
        var urlCallback = linkGeneratorService.GenerateWebApp(UrlsWebApp.EmailRegisterValidate);

        var model = new ValidateEmailRegistrationViewModel
        {
            CompanyName = company.Name, Email = user.Email, Url = urlCallback, Code = code
        };

        emailService.Subject = localizer["Confirmación de email en Employee Control."];
        emailService.To.Add(model.Email.SetEmptyIfNull());
        emailService.IsBodyHtml = true;
        await emailService.SendMailWithViewAsync(EmailViews.ValidateEmailRegistration, model);
    }
}
