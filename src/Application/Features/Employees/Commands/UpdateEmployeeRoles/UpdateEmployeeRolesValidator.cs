using FluentValidation;

namespace EmployeeControl.Application.Features.Employees.Commands.UpdateEmployeeRoles;

internal class UpdateEmployeeRolesValidator : AbstractValidator<UpdateEmployeeRolesCommand>
{
    public UpdateEmployeeRolesValidator()
    {
        RuleFor(r => r.EmployeeId)
            .NotEmpty();
    }
}
