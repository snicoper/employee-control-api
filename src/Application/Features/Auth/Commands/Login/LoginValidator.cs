﻿using FluentValidation;

namespace EmployeeControl.Application.Features.Auth.Commands.Login;

public class LoginValidator : AbstractValidator<LoginCommand>
{
    public LoginValidator()
    {
        RuleFor(r => r.Identifier)
            .NotEmpty();

        RuleFor(r => r.Password)
            .NotEmpty();
    }
}