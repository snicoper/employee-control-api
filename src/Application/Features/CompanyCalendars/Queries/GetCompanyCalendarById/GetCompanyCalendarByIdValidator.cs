using FluentValidation;

namespace EmployeeControl.Application.Features.CompanyCalendars.Queries.GetCompanyCalendarById;

internal class GetCompanyCalendarByIdValidator : AbstractValidator<GetCompanyCalendarByIdQuery>
{
    public GetCompanyCalendarByIdValidator()
    {
        RuleFor(r => r.Id)
            .NotEmpty();
    }
}
