using FluentValidation;

namespace EmployeeControl.Application.Features.TimesControl.Commands.CloseIncidence;

internal class CloseIncidenceValidator : AbstractValidator<CloseIncidenceCommand>
{
    public CloseIncidenceValidator()
    {
        RuleFor(r => r.Id)
            .NotEmpty();
    }
}
