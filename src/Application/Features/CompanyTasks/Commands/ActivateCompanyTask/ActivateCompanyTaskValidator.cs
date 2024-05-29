using FluentValidation;

namespace EmployeeControl.Application.Features.CompanyTasks.Commands.ActivateCompanyTask;

internal class ActivateCompanyTaskValidator : AbstractValidator<ActivateCompanyTaskCommand>
{
    public ActivateCompanyTaskValidator()
    {
        RuleFor(r => r.CompanyTaskId)
            .NotEmpty();
    }
}
