﻿using FluentValidation;

namespace EmployeeControl.Application.Cqrs.Identity.Commands.Login;

public class LoginValidator : AbstractValidator<LoginCommand>
{
    public LoginValidator()
    {
        RuleFor(r => r.UserName)
            .NotEmpty();

        RuleFor(r => r.Password)
            .NotEmpty();
    }
}
