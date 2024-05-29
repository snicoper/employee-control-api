using FluentValidation;

namespace EmployeeControl.Application.Features.Departments.Queries.GetDepartmentById;

internal class GetDepartmentByIdValidator : AbstractValidator<GetDepartmentByIdQuery>
{
    public GetDepartmentByIdValidator()
    {
        RuleFor(r => r.DepartmentId)
            .NotEmpty();
    }
}
