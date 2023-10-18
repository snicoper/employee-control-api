using EmployeeControl.Application.Common.Constants;
using EmployeeControl.Application.Common.Extensions;
using EmployeeControl.Application.Common.Interfaces;
using EmployeeControl.Application.Common.Interfaces.Entities.Identity;
using EmployeeControl.Application.Common.Models.Emails;
using EmployeeControl.Application.Common.Models.Settings;
using EmployeeControl.Application.Localizations;
using EmployeeControl.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace EmployeeControl.Infrastructure.Services.Entities.Identity;

public class AuthEmailsService(
        UserManager<ApplicationUser> userManager,
        IEmailService emailService,
        ILinkGeneratorService linkGeneratorService,
        IStringLocalizer<SharedLocalizer> localizer,
        IOptions<WebApiSettings> webApiSettings)
    : IAuthEmailsService
{
    public async Task SendValidateEmailAsync(ApplicationUser user, Domain.Entities.Company company)
    {
        // Generar code de validación.
        var code = await userManager.GenerateEmailConfirmationTokenAsync(user);

        // Url validación.
        var queryParams = new Dictionary<string, string> { ["userId"] = user.Id, ["code"] = code };
        var urlCallback = linkGeneratorService.GenerateWebApp(UrlsWebApp.EmailRegisterValidate, queryParams);

        var model = new ValidateEmailRegistrationViewModel
        {
            CompanyName = company.Name,
            Email = user.Email,
            UrlValidate = urlCallback,
            SiteName = webApiSettings.Value.SiteName
        };

        emailService.Subject = localizer["Confirmación de email en Employee Control."];
        emailService.To.Add(model.Email.SetEmptyIfNull());
        emailService.IsBodyHtml = true;
        await emailService.SendMailWithViewAsync(EmailViews.ValidateEmailRegistration, model);
    }
}
