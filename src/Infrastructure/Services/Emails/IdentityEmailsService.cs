using EmployeeControl.Application.Common.Constants;
using EmployeeControl.Application.Common.Interfaces.Common;
using EmployeeControl.Application.Common.Interfaces.Emails;
using EmployeeControl.Application.Common.Localization;
using EmployeeControl.Application.Common.Models.Emails;
using EmployeeControl.Application.Common.Models.Settings;
using EmployeeControl.Domain.Entities;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace EmployeeControl.Infrastructure.Services.Emails;

public class IdentityEmailsService(
    IEmailService emailService,
    ILinkGeneratorService linkGeneratorService,
    IStringLocalizer<SharedResource> localizer,
    IOptions<WebApiSettings> webApiSettings)
    : IIdentityEmailsService
{
    public async Task SendInviteEmployeeAsync(User user, Company company, string code)
    {
        // Url validación.
        var queryParams = new Dictionary<string, string> { ["userId"] = user.Id.ToString(), ["code"] = code };
        var callback = linkGeneratorService.GenerateWebApp(UrlsWebApp.InviteEmployee, queryParams);

        // View model.
        var model = new InviteEmployeeViewModel(company.Name, callback, webApiSettings.Value.SiteName);

        // Send email.
        emailService.Subject = localizer["Invitación de la empresa {0}.", company.Name];
        emailService.To.Add(user.Email ?? string.Empty);

        await emailService.SendMailWithViewAsync(NameEmailViews.InviteEmployee, model);
    }

    public async Task SendRecoveryPasswordAsync(User user, string code)
    {
        // Url validación.
        var queryParams = new Dictionary<string, string> { ["userId"] = user.Id.ToString(), ["code"] = code };
        var callback = linkGeneratorService.GenerateWebApp(UrlsWebApp.RecoveryPasswordChange, queryParams);

        // View model.
        var recoveryPasswordViewModel = new RecoveryPasswordViewModel(webApiSettings.Value.SiteName, callback);

        // Send email.
        emailService.Subject = localizer[
            "Confirmación de cambio de email en {0}.",
            webApiSettings.Value.SiteName ?? string.Empty];

        emailService.To.Add(user.Email ?? string.Empty);

        await emailService.SendMailWithViewAsync(NameEmailViews.RecoveryPassword, recoveryPasswordViewModel);
    }
}
