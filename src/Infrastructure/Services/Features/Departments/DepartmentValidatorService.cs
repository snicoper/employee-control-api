﻿using EmployeeControl.Application.Common.Interfaces.Common;
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
            .AnyAsync(
                d => d.CompanyId == department.CompanyId &&
                     d.Name.ToLower().Equals(department.Name.ToLower()),
                cancellationToken);

        if (!company)
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
            .AnyAsync(
                d => d.CompanyId == department.CompanyId &&
                     d.Background.ToLower().Equals(department.Background.ToLower()) &&
                     d.Color.ToLower().Equals(department.Color.ToLower()),
                cancellationToken);

        if (!company)
        {
            return;
        }

        var errorMessage = localizer["Ya existe una combinación de colores así en los departamentos."];
        validationFailureService.Add(nameof(Department.Background), errorMessage);
        validationFailureService.Add(nameof(Department.Color), errorMessage);
    }
}
