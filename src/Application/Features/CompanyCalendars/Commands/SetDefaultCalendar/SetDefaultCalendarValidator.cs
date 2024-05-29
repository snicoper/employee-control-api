using FluentValidation;

namespace EmployeeControl.Application.Features.CompanyCalendars.Commands.SetDefaultCalendar;

internal class SetDefaultCalendarValidator : AbstractValidator<SetDefaultCalendarCommand>
{
    public SetDefaultCalendarValidator()
    {
        RuleFor(r => r.Id)
            .NotEmpty();
    }
}
