using FluentValidation;

namespace EmployeeControl.Application.Features.WorkingDaysWeek.Commands.UpdateWorkingDaysWeek;

internal class UpdateWorkingDaysWeekValidator : AbstractValidator<UpdateWorkingDaysWeekCommand>
{
    public UpdateWorkingDaysWeekValidator()
    {
        RuleFor(r => r.Id)
            .NotEmpty();
    }
}
