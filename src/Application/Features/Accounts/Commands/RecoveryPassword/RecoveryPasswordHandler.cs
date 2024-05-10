using EmployeeControl.Application.Common.Interfaces.Features.Identity;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Localization;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace EmployeeControl.Application.Features.Accounts.Commands.RecoveryPassword;

internal class RecoveryPasswordHandler(
    IIdentityService identityService,
    UserManager<User> userManager,
    IIdentityEmailsService identityEmailsService,
    IStringLocalizer<IdentityResource> localizer)
    : ICommandHandler<RecoveryPasswordCommand>
{
    public async Task<Result> Handle(RecoveryPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await identityService.GetByEmailAsync(request.Email);

        // El usuario debe estar activo.
        if (!user.Active)
        {
            var messageError = localizer["La cuenta no esta activa."];
            var result = Result.Failure(nameof(user.Email), messageError);

            result.RaiseBadRequest();
        }

        // El usuario ha debido confirmar el email.
        if (!user.EmailConfirmed)
        {
            var messageError = localizer["Correo electrónico no confirmado."];
            var result = Result.Failure(nameof(user.Email), messageError);

            result.RaiseBadRequest();
        }

        // Generar code de validación y enviar correo electrónico.
        var code = await userManager.GeneratePasswordResetTokenAsync(user);
        await identityEmailsService.SendRecoveryPasswordAsync(user, code);

        return Result.Success();
    }
}
