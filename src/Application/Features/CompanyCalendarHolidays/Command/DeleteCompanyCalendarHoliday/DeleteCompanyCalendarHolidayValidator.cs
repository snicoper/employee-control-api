using FluentValidation;

namespace EmployeeControl.Application.Features.CompanyCalendarHolidays.Command.DeleteCompanyCalendarHoliday;

internal class DeleteCompanyCalendarHolidayValidator : AbstractValidator<DeleteCompanyCalendarHolidayCommand>
{
    public DeleteCompanyCalendarHolidayValidator()
    {
        RuleFor(r => r.Id)
            .NotEmpty();
    }
}
