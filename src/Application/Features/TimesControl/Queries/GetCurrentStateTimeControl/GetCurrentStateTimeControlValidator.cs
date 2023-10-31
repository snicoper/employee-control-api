using FluentValidation;

namespace EmployeeControl.Application.Features.TimesControl.Queries.GetCurrentStateTimeControl;

public class GetCurrentStateTimeControlValidator : AbstractValidator<GetCurrentStateTimeControlQuery>
{
    public GetCurrentStateTimeControlValidator()
    {
        RuleFor(r => r.EmployeeId)
            .NotEmpty();
    }
}
