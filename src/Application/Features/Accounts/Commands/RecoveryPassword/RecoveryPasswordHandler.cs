using EmployeeControl.Application.Common.Exceptions;
using EmployeeControl.Application.Common.Interfaces.Features.Identity;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EmployeeControl.Application.Features.Accounts.Commands.RecoveryPassword;

internal class RecoveryPasswordHandler(UserManager<ApplicationUser> userManager, IIdentityEmailsService identityEmailsService)
    : IRequestHandler<RecoveryPasswordCommand, Result>
{
    public async Task<Result> Handle(RecoveryPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email) ??
                   throw new NotFoundException(nameof(ApplicationUser), nameof(ApplicationUser.Email));

        // Para restablecer contraseña, el usuario ha debido confirmar el email.
        if (!user.EmailConfirmed)
        {
            return Result.Failure("Correo electrónico no confirmado.");
        }

        // Generar code de validación y enviar correo electrónico.
        var code = await userManager.GeneratePasswordResetTokenAsync(user);
        await identityEmailsService.SendRecoveryPasswordAsync(user, code);

        return Result.Success();
    }
}
