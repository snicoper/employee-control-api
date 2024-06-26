﻿using EmployeeControl.Application.Common.Localization;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace EmployeeControl.Application.Features.Accounts.Commands.RestorePassword;

internal class RestorePasswordValidator : AbstractValidator<RestorePasswordCommand>
{
    public RestorePasswordValidator(IStringLocalizer<IdentityResource> localizer)
    {
        RuleFor(r => r.Password)
            .NotEmpty()
            .MinimumLength(6)
            .Equal(r => r.ConfirmPassword)
            .WithMessage(localizer["Las contraseñas no coinciden."]);

        RuleFor(r => r.ConfirmPassword)
            .NotEmpty()
            .Equal(r => r.Password)
            .WithMessage(localizer["Las contraseñas no coinciden."]);
    }
}
