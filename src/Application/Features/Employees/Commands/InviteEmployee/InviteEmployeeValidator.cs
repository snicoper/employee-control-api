﻿using FluentValidation;

namespace EmployeeControl.Application.Features.Employees.Commands.InviteEmployee;

internal class InviteEmployeeValidator : AbstractValidator<InviteEmployeeCommand>
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

        RuleFor(r => r.TimeZone)
            .NotEmpty();

        RuleFor(r => r.CompanyCalendarId)
            .NotEmpty();
    }
}
