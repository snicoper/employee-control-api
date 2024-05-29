using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Localization;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Entities;
using EmployeeControl.Domain.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace EmployeeControl.Infrastructure.Validators;

public class DepartmentValidator(
    IApplicationDbContext context,
    IStringLocalizer<DepartmentResource> localizer)
    : IDepartmentValidator
{
    public async Task<Result> ValidateNameAsync(Department department, Result result, CancellationToken cancellationToken)
    {
        var company = await context
            .Departments
            .AnyAsync(
                d => string.Equals(d.Name.ToLower(), department.Name.ToLower()) && d.Id != department.Id,
                cancellationToken);

        if (!company)
        {
            return result;
        }

        var errorMessage = localizer["El nombre de departamento ya existe."];
        result.AddError(nameof(Department.Name), errorMessage);

        return result;
    }

    public async Task<Result> ValidateBackgroundAndColorAsync(
        Department department,
        Result result,
        CancellationToken cancellationToken)
    {
        var company = await context
            .Departments
            .AnyAsync(
                d => string.Equals(d.Background.ToLower(), department.Background.ToLower()) &&
                     string.Equals(d.Color.ToLower(), department.Color.ToLower()) &&
                     d.Id != department.Id,
                cancellationToken);

        if (!company)
        {
            return result;
        }

        var errorMessage = localizer["Ya existe una combinación de colores así en los departamentos."];
        result.AddError(nameof(Department.Background), errorMessage);
        result.AddError(nameof(Department.Color), errorMessage);

        return result;
    }
}
