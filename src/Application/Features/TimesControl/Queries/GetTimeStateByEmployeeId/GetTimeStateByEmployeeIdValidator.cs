using FluentValidation;

namespace EmployeeControl.Application.Features.TimesControl.Queries.GetTimeStateByEmployeeId;

internal class GetTimeStateByEmployeeIdValidator : AbstractValidator<GetTimeStateByEmployeeIdQuery>
{
    public GetTimeStateByEmployeeIdValidator()
    {
        RuleFor(r => r.EmployeeId)
            .NotEmpty();
    }
}
