﻿using FluentValidation;

namespace EmployeeControl.Application.Features.Auth.Commands.RefreshToken;

internal class RefreshTokenValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenValidator()
    {
        RuleFor(r => r.RefreshToken)
            .NotEmpty()
            .MaximumLength(50);
    }
}
