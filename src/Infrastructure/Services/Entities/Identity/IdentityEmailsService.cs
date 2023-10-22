using EmployeeControl.Application.Common.Constants;
using EmployeeControl.Application.Common.Extensions;
using EmployeeControl.Application.Common.Interfaces.Common;
using EmployeeControl.Application.Common.Interfaces.Emails;
using EmployeeControl.Application.Common.Interfaces.Entities.Identity;
using EmployeeControl.Application.Common.Models.Emails;
using EmployeeControl.Application.Common.Models.Settings;
using EmployeeControl.Application.Localizations;
using EmployeeControl.Domain.Entities;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace EmployeeControl.Infrastructure.Services.Entities.Identity;

public class IdentityEmailsService(
        IEmailService emailService,
        ILinkGeneratorService linkGeneratorService,
        IStringLocalizer<SharedLocalizer> localizer,
        IOptions<WebApiSettings> webApiSettings)
    : IIdentityEmailsService
{
    public async Task SendValidateEmailAsync(ApplicationUser user, Domain.Entities.Company company, string code)
    {
        // Url validación.
        var queryParams = new Dictionary<string, string> { ["userId"] = user.Id, ["code"] = code };
        var callback = linkGeneratorService.GenerateWebApp(UrlsWebApp.EmailRegisterValidate, queryParams);

        // View model.
        var model = new ValidateEmailRegistrationViewModel
        {
            CompanyName = company.Name, Email = user.Email, Callback = callback, SiteName = webApiSettings.Value.SiteName
        };

        // Send email.
        emailService.Subject = localizer[
            "Confirmación de correo electrónico en {0}.",
            webApiSettings.Value.SiteName.ToEmptyIfNull()];

        emailService.To.Add(model.Email.ToEmptyIfNull());

        await emailService.SendMailWithViewAsync(EmailViews.ValidateEmailRegistration, model);
    }

    public async Task SendInviteEmployeeAsync(ApplicationUser user, Domain.Entities.Company company, string code)
    {
        // Url validación.
        var queryParams = new Dictionary<string, string> { ["userId"] = user.Id, ["code"] = code };
        var callback = linkGeneratorService.GenerateWebApp(UrlsWebApp.InviteEmployee, queryParams);

        // View model.
        var model = new InviteEmployeeViewModel
        {
            CompanyName = company.Name, Callback = callback, SiteName = webApiSettings.Value.SiteName
        };

        // Send email.
        emailService.Subject = localizer["Invitación de la empresa {0}.", company.Name.ToEmptyIfNull()];
        emailService.To.Add(user.Email.ToEmptyIfNull());

        await emailService.SendMailWithViewAsync(EmailViews.InviteEmployee, model);
    }

    public async Task SendRecoveryPasswordAsync(ApplicationUser user, string code)
    {
        // Url validación.
        var queryParams = new Dictionary<string, string> { ["userId"] = user.Id, ["code"] = code };
        var callback = linkGeneratorService.GenerateWebApp(UrlsWebApp.RecoveryPasswordChange, queryParams);

        // View model.
        var recoveryPasswordViewModel = new RecoveryPasswordViewModel
        {
            SiteName = webApiSettings.Value.SiteName, CallBack = callback
        };

        // Send email.
        emailService.Subject = localizer[
            "Confirmación de cambio de email en {0}.",
            webApiSettings.Value.SiteName.ToEmptyIfNull()];

        emailService.To.Add(user.Email.ToEmptyIfNull());

        await emailService.SendMailWithViewAsync(EmailViews.RecoveryPassword, recoveryPasswordViewModel);
    }
}
