using EmployeeControl.Application.Features.TimesControl.Commands.StartTimeControl;
using FluentValidation;

namespace EmployeeControl.Application.Features.TimesControl.Commands.FinishTimeControl;

internal class FinishTimeControlValidator : AbstractValidator<StartTimeControlCommand>
{
    public FinishTimeControlValidator()
    {
        RuleFor(r => r.EmployeeId)
            .NotEmpty();
    }
}
