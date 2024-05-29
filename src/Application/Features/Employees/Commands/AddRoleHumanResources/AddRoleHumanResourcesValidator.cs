using FluentValidation;

namespace EmployeeControl.Application.Features.Employees.Commands.AddRoleHumanResources;

internal class AddRoleHumanResourcesValidator : AbstractValidator<AddRoleHumanResourcesCommand>
{
    public AddRoleHumanResourcesValidator()
    {
        RuleFor(r => r.EmployeeId)
            .NotEmpty();
    }
}
