using FluentValidation;

namespace EmployeeControl.Application.Features.TimeControl.Commands.StartTimeControl;

public class StartTimeControlValidator : AbstractValidator<StartTimeControlCommand>
{
    public StartTimeControlValidator()
    {
        RuleFor(r => r.EmployeeId)
            .NotEmpty();
    }
}
