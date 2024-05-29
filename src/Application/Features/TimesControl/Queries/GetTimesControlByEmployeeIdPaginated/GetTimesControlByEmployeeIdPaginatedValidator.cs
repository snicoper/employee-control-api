using FluentValidation;

namespace EmployeeControl.Application.Features.TimesControl.Queries.GetTimesControlByEmployeeIdPaginated;

internal class GetTimesControlByEmployeeIdPaginatedValidator : AbstractValidator<GetTimesControlByEmployeeIdPaginatedQuery>
{
    public GetTimesControlByEmployeeIdPaginatedValidator()
    {
        RuleFor(r => r.EmployeeId)
            .NotEmpty();
    }
}
