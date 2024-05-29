using FluentValidation;

namespace EmployeeControl.Application.Features.CompanyTasks.Commands.DeactivateCompanyTask;

internal class DeactivateCompanyTaskValidator : AbstractValidator<DeactivateCompanyTaskCommand>
{
    public DeactivateCompanyTaskValidator()
    {
        RuleFor(r => r.CompanyTaskId)
            .NotEmpty();
    }
}
