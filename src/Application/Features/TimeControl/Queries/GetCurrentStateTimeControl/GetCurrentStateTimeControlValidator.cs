using FluentValidation;

namespace EmployeeControl.Application.Features.TimeControl.Queries.GetCurrentStateTimeControl;

public class GetCurrentStateTimeControlValidator : AbstractValidator<GetCurrentStateTimeControlQuery>
{
    public GetCurrentStateTimeControlValidator()
    {
        RuleFor(r => r.EmployeeId)
            .NotEmpty();
    }
}
