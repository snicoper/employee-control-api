using FluentValidation;

namespace EmployeeControl.Application.Features.TimesControl.Queries.GetTimeControlWithEmployeeById;

internal class GetTimeControlWithEmployeeByIdValidator : AbstractValidator<GetTimeControlWithEmployeeByIdQuery>
{
    public GetTimeControlWithEmployeeByIdValidator()
    {
        RuleFor(r => r.Id)
            .NotEmpty();
    }
}
