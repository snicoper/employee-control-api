﻿using FluentValidation;

namespace EmployeeControl.Application.Features.CompanyTasks.Commands.CreateCompanyTask;

public class CreateCompanyTaskValidator : AbstractValidator<CreateCompanyTaskCommand>
{
    public CreateCompanyTaskValidator()
    {
        RuleFor(r => r.CompanyId)
            .NotEmpty();

        RuleFor(r => r.Name)
            .NotEmpty()
            .MaximumLength(256);
    }
}
