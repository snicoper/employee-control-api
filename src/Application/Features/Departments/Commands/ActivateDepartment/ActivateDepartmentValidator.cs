using FluentValidation;

namespace EmployeeControl.Application.Features.Departments.Commands.ActivateDepartment;

internal class ActivateDepartmentValidator : AbstractValidator<ActivateDepartmentCommand>
{
    public ActivateDepartmentValidator()
    {
        RuleFor(r => r.DepartmentId)
            .NotEmpty();
    }
}
