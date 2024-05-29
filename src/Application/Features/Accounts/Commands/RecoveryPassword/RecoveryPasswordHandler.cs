using EmployeeControl.Application.Common.Extensions;
using EmployeeControl.Application.Common.Interfaces.Emails;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Localization;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Entities;
using EmployeeControl.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace EmployeeControl.Application.Features.Accounts.Commands.RecoveryPassword;

internal class RecoveryPasswordHandler(
    UserManager<User> userManager,
    IUserRepository userRepository,
    IIdentityEmailsService identityEmailsService,
    IStringLocalizer<IdentityResource> localizer)
    : ICommandHandler<RecoveryPasswordCommand>
{
    public async Task<Result> Handle(RecoveryPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailAsync(request.Email);

        if (!user.Active)
        {
            var errorMessage = localizer["La cuenta no esta activa."];
            var result = Result.Failure(nameof(user.Email), errorMessage);

            result.RaiseBadRequest();
        }

        if (!user.EmailConfirmed)
        {
            var errorMessage = localizer["Correo electrónico no confirmado."];
            var result = Result.Failure(nameof(user.Email), errorMessage);

            result.RaiseBadRequest();
        }

        var code = await userManager.GeneratePasswordResetTokenAsync(user);
        await identityEmailsService.SendRecoveryPasswordAsync(user, code);

        return Result.Success();
    }
}
