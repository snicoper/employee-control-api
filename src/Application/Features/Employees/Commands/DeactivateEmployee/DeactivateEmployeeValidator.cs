using FluentValidation;

namespace EmployeeControl.Application.Features.Employees.Commands.DeactivateEmployee;

internal class DeactivateEmployeeValidator : AbstractValidator<DeactivateEmployeeCommand>
{
    public DeactivateEmployeeValidator()
    {
        RuleFor(r => r.EmployeeId)
            .NotEmpty();
    }
}
