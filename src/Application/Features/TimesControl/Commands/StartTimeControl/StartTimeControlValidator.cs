using FluentValidation;

namespace EmployeeControl.Application.Features.TimesControl.Commands.StartTimeControl;

internal class StartTimeControlValidator : AbstractValidator<StartTimeControlCommand>
{
    public StartTimeControlValidator()
    {
        RuleFor(r => r.EmployeeId)
            .NotEmpty();
    }
}
