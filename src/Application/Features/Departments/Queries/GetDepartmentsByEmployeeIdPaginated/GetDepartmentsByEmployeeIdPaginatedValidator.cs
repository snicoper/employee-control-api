using FluentValidation;

namespace EmployeeControl.Application.Features.Departments.Queries.GetDepartmentsByEmployeeIdPaginated;

internal class GetDepartmentsByEmployeeIdPaginatedValidator : AbstractValidator<GetDepartmentsByEmployeeIdPaginatedQuery>
{
    public GetDepartmentsByEmployeeIdPaginatedValidator()
    {
        RuleFor(r => r.EmployeeId)
            .NotEmpty();
    }
}
