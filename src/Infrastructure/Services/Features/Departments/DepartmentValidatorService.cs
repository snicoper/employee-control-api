using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Features.Departments;
using EmployeeControl.Application.Common.Localization;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace EmployeeControl.Infrastructure.Services.Features.Departments;

public class DepartmentValidatorService(
    IApplicationDbContext context,
    IStringLocalizer<DepartmentResource> localizer)
    : IDepartmentValidatorService
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
            return Result.Success();
        }

        var errorMessage = localizer["Ya existe una combinación de colores así en los departamentos."];
        result.AddError(nameof(Department.Background), errorMessage);
        result.AddError(nameof(Department.Color), errorMessage);

        return result;
    }
}
