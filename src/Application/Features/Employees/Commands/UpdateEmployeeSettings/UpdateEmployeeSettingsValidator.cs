using FluentValidation;

namespace EmployeeControl.Application.Features.Employees.Commands.UpdateEmployeeSettings;

internal class UpdateEmployeeSettingsValidator : AbstractValidator<UpdateEmployeeSettingsCommand>
{
    public UpdateEmployeeSettingsValidator()
    {
        RuleFor(r => r.Id)
            .NotEmpty();

        RuleFor(r => r.UserId)
            .NotEmpty();

        RuleFor(r => r.Timezone)
            .NotEmpty();
    }
}
