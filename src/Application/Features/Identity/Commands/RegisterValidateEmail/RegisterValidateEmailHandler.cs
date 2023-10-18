using EmployeeControl.Application.Common.Constants;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Application.Localizations;
using EmployeeControl.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeControl.Application.Features.Identity.Commands.RegisterValidateEmail;

internal class RegisterValidateEmailHandler(
        UserManager<ApplicationUser> userManager,
        ILogger<RegisterValidateEmailHandler> logger,
        IStringLocalizer<IdentityLocalizer> localizer)
    : IRequestHandler<RegisterValidateEmailCommand, Result>
{
    public async Task<Result> Handle(RegisterValidateEmailCommand request, CancellationToken cancellationToken)
    {
        var code = Base64UrlEncoder.Decode(request.Code);
        var userId = Base64UrlEncoder.Decode(request.UserId);

        var user = await userManager.FindByIdAsync(userId);

        string message;

        if (user is null)
        {
            message = localizer["El usuario no ha sido encontrado."];
            logger.LogDebug("{message}", message);

            return Result.Failure(new[] { ValidationErrorsKeys.Identity, message });
        }

        var confirmEmailResult = await userManager.ConfirmEmailAsync(user, code);

        if (confirmEmailResult.Succeeded)
        {
            return Result.Success();
        }

        message = localizer["El tiempo de validación ha expirado."];
        logger.LogDebug("{message}", message);

        return Result.Failure(new[] { ValidationErrorsKeys.Identity, message });
    }
}
