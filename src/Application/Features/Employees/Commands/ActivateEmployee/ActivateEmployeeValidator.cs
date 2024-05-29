using FluentValidation;

namespace EmployeeControl.Application.Features.Employees.Commands.ActivateEmployee;

internal class ActivateEmployeeValidator : AbstractValidator<ActivateEmployeeCommand>
{
    public ActivateEmployeeValidator()
    {
        RuleFor(r => r.EmployeeId)
            .NotEmpty();
    }
}
