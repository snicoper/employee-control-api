using EmployeeControl.Application.Common.Models;
using EmployeeControl.Application.Localizations;
using EmployeeControl.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeControl.Application.Features.Identity.Commands.RecoveryPasswordChange;

internal class RecoveryPasswordChangeHandler(
        UserManager<ApplicationUser> userManager,
        IStringLocalizer<IdentityLocalizer> localizer,
        ILogger<RecoveryPasswordChangeHandler> logger)
    : IRequestHandler<RecoveryPasswordChangeCommand, Result>
{
    public async Task<Result> Handle(RecoveryPasswordChangeCommand request, CancellationToken cancellationToken)
    {
        var code = Base64UrlEncoder.Decode(request.Code);
        var userId = Base64UrlEncoder.Decode(request.UserId);

        var user = await userManager.FindByIdAsync(userId);

        if (user is null)
        {
            var message = localizer["El usuario no ha sido encontrado."];
            logger.LogDebug("{message}", message);

            return Result.Failure(message);
        }

        var resetResult = await userManager.ResetPasswordAsync(user, code, request.Password);

        return !resetResult.Succeeded ? Result.Failure(localizer["Error al cambiar la contraseña"]) : Result.Success();
    }
}
