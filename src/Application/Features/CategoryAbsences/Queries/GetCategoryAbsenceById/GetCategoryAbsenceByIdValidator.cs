using FluentValidation;

namespace EmployeeControl.Application.Features.CategoryAbsences.Queries.GetCategoryAbsenceById;

internal class GetCategoryAbsenceByIdValidator : AbstractValidator<GetCategoryAbsenceByIdQuery>
{
    public GetCategoryAbsenceByIdValidator()
    {
        RuleFor(r => r.Id)
            .NotEmpty();
    }
}
