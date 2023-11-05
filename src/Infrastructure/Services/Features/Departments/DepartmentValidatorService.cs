using EmployeeControl.Application.Common.Interfaces.Common;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Features.Departments;
using EmployeeControl.Application.Localizations;
using EmployeeControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace EmployeeControl.Infrastructure.Services.Features.Departments;

public class DepartmentValidatorService(
    IApplicationDbContext context,
    IValidationFailureService validationFailureService,
    IStringLocalizer<DepartmentLocalization> localizer)
    : IDepartmentValidatorService
{
    public async Task ValidateNameAsync(Department department, CancellationToken cancellationToken)
    {
        var company = await context
            .Departments
            .AsNoTracking()
            .SingleOrDefaultAsync(
                d => d.CompanyId == department.CompanyId &&
                     string.Equals(d.Name.ToLower(), department.Name.ToLower(), StringComparison.Ordinal),
                cancellationToken);

        if (company is null)
        {
            return;
        }

        var errorMessage = localizer["El nombre de departamento ya existe."];
        validationFailureService.Add(nameof(Department.Name), errorMessage);
    }

    public async Task ValidateBackgroundAndColorAsync(Department department, CancellationToken cancellationToken)
    {
        var company = await context
            .Departments
            .AsNoTracking()
            .SingleOrDefaultAsync(
                d => d.CompanyId == department.CompanyId &&
                     string.Equals(d.Background.ToLower(), department.Background.ToLower(), StringComparison.Ordinal) &&
                     string.Equals(d.Color.ToLower(), department.Color.ToLower(), StringComparison.Ordinal),
                cancellationToken);

        if (company is null)
        {
            return;
        }

        var errorMessage = localizer["Ya existe una combinación de colores así en los departamentos."];
        validationFailureService.Add(nameof(Department.Background), errorMessage);
        validationFailureService.Add(nameof(Department.Color), errorMessage);
    }
}
