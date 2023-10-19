using EmployeeControl.Application.Common.Extensions;
using EmployeeControl.Application.Common.Interfaces.Entities.Identity;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EmployeeControl.Application.Features.Identity.Commands.RecoveryPassword;

internal class RecoveryPasswordHandler(UserManager<ApplicationUser> userManager, IIdentityEmailsService identityEmailsService)
    : IRequestHandler<RecoveryPasswordCommand, Result>
{
    public async Task<Result> Handle(RecoveryPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email.SetEmptyIfNull());

        if (user is null)
        {
            return Result.Failure("error");
        }

        await identityEmailsService.SendRecoveryPasswordAsync(user);

        return Result.Success();
    }
}
