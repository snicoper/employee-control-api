using FluentValidation;

namespace EmployeeControl.Application.Features.CompanyCalendarHolidays.Queries.
    GetCompanyCalendarHolidaysByCompanyCalendarIdAndYear;

internal class GetCompanyCalendarHolidaysByCompanyCalendarIdAndYearValidator
    : AbstractValidator<GetCompanyCalendarHolidaysByCompanyCalendarIdAndYearQuery>
{
    public GetCompanyCalendarHolidaysByCompanyCalendarIdAndYearValidator()
    {
        RuleFor(r => r.CompanyCalendarId)
            .NotEmpty();

        RuleFor(r => r.Year)
            .NotEmpty();
    }
}
