using FluentValidation;

namespace EmployeeControl.Application.Features.TimesControl.Commands.DeleteTimeControl;

internal class DeleteTimeControlValidator : AbstractValidator<DeleteTimeControlCommand>
{
    public DeleteTimeControlValidator()
    {
        RuleFor(r => r.Id)
            .NotEmpty();
    }
}
