using EmployeeControl.Application.Features.TimeControl.Commands.StartTimeControl;
using FluentValidation;

namespace EmployeeControl.Application.Features.TimeControl.Commands.FinishTimeControl;

public class FinishTimeControlValidator : AbstractValidator<StartTimeControlCommand>
{
    public FinishTimeControlValidator()
    {
        RuleFor(r => r.EmployeeId)
            .NotEmpty();
    }
}
