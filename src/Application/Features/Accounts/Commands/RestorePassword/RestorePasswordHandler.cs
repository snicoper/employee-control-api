﻿using EmployeeControl.Application.Common.Constants;
using EmployeeControl.Application.Common.Extensions;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Localization;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeControl.Application.Features.Accounts.Commands.RestorePassword;

internal class RestorePasswordHandler(
    UserManager<User> userManager,
    IStringLocalizer<IdentityResource> localizer,
    ILogger<RestorePasswordHandler> logger)
    : ICommandHandler<RestorePasswordCommand>
{
    public async Task<Result> Handle(RestorePasswordCommand request, CancellationToken cancellationToken)
    {
        var code = Base64UrlEncoder.Decode(request.Code);
        var userId = Base64UrlEncoder.Decode(request.UserId.ToString());
        var result = Result.Create();

        var user = await userManager.FindByIdAsync(userId);

        if (user is null)
        {
            var message = localizer["El usuario no existe."];
            logger.LogDebug("{Message}", message);
            result.AddError(ValidationErrorTypes.NonFieldErrors, message);
            result.RaiseBadRequestIfErrorsExist();
        }

        if (!user!.EmailConfirmed)
        {
            var message = localizer["Correo electrónico no confirmado."];
            result.AddError(ValidationErrorTypes.NonFieldErrors, message);
            result.RaiseBadRequestIfErrorsExist();
        }

        if (!user.Active)
        {
            var message = localizer["La cuenta no esta activa."];
            result.AddError(ValidationErrorTypes.NonFieldErrors, message);
            result.RaiseBadRequestIfErrorsExist();
        }

        var resetResult = await userManager.ResetPasswordAsync(user, code, request.Password);
        var resultResponse = resetResult.Succeeded
            ? Result.Success()
            : Result.Failure(ValidationErrorTypes.NonFieldErrors, localizer["Error al cambiar la contraseña."]);

        return resultResponse;
    }
}
