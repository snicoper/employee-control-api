using FluentValidation;

namespace EmployeeControl.Application.Features.Departments.Commands.DeactivateDepartment;

internal class DeactivateDepartmentValidator : AbstractValidator<DeactivateDepartmentCommand>
{
    public DeactivateDepartmentValidator()
    {
        RuleFor(r => r.DepartmentId)
            .NotEmpty();
    }
}
