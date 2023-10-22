﻿using FluentValidation;

namespace EmployeeControl.Application.Features.Employee.Commands.InviteEmployee;

public class InviteEmployeeValidator : AbstractValidator<InviteEmployeeCommand>
{
    public InviteEmployeeValidator()
    {
        RuleFor(r => r.FirstName)
            .NotEmpty();

        RuleFor(r => r.LastName)
            .NotEmpty();

        RuleFor(r => r.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(r => r.CompanyId)
            .NotEmpty()
            .GreaterThan(0);
    }
}
