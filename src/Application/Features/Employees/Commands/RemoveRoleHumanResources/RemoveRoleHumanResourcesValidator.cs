using FluentValidation;

namespace EmployeeControl.Application.Features.Employees.Commands.RemoveRoleHumanResources;

internal class RemoveRoleHumanResourcesValidator : AbstractValidator<RemoveRoleHumanResourcesCommand>
{
    public RemoveRoleHumanResourcesValidator()
    {
        RuleFor(r => r.EmployeeId)
            .NotEmpty();
    }
}
