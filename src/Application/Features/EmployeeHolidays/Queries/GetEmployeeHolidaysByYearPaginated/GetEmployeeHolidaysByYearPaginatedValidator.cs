using FluentValidation;

namespace EmployeeControl.Application.Features.EmployeeHolidays.Queries.GetEmployeeHolidaysByYearPaginated;

internal class GetEmployeeHolidaysByYearPaginatedValidator : AbstractValidator<GetEmployeeHolidaysByYearPaginatedQuery>
{
    public GetEmployeeHolidaysByYearPaginatedValidator()
    {
        RuleFor(r => r.Year)
            .NotEmpty();
    }
}
