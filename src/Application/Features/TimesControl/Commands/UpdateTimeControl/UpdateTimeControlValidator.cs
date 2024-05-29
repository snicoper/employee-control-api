﻿using FluentValidation;

namespace EmployeeControl.Application.Features.TimesControl.Commands.UpdateTimeControl;

internal class UpdateTimeControlValidator : AbstractValidator<UpdateTimeControlCommand>
{
    public UpdateTimeControlValidator()
    {
        RuleFor(r => r.Id)
            .NotEmpty();

        RuleFor(r => r.Start)
            .NotEmpty()
            .NotNull();
    }
}
