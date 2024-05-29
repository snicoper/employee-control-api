﻿using FluentValidation;

namespace EmployeeControl.Application.Features.TimesControl.Commands.CreateIncidence;

internal class CreateIncidenceValidator : AbstractValidator<CreateIncidenceCommand>
{
    public CreateIncidenceValidator()
    {
        RuleFor(r => r.TimeControlId)
            .NotEmpty();

        RuleFor(r => r.IncidenceDescription)
            .MaximumLength(256)
            .NotEmpty();
    }
}
