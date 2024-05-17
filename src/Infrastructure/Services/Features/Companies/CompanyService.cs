﻿using EmployeeControl.Application.Common.Interfaces.Features.Companies;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Entities;
using EmployeeControl.Domain.Exceptions;
using EmployeeControl.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl.Infrastructure.Services.Features.Companies;

public class CompanyService(ApplicationDbContext context, ICompanyValidatorService companyValidatorService)
    : ICompanyService
{
    public async Task<Company> GetCompanyAsync(CancellationToken cancellationToken)
    {
        var result = await context.Companies.FirstOrDefaultAsync(cancellationToken)
                     ?? throw new NotFoundException(nameof(Company), nameof(Company));

        return result;
    }

    public async Task<Company> GetCompanyByIdAsync(string companyId, CancellationToken cancellationToken)
    {
        var result = await context.Companies.SingleOrDefaultAsync(c => c.Id == companyId, cancellationToken)
                     ?? throw new NotFoundException(nameof(Company), nameof(Company));

        return result;
    }

    public async Task<Company> CreateAsync(Company company, string timezone, CancellationToken cancellationToken)
    {
        // Validaciones.
        var result = Result.Create();
        result = await companyValidatorService.UniqueNameValidationAsync(company.Name, result, cancellationToken);
        result.RaiseBadRequestIfResultFailure();

        // Crear Company y establecer valores de CompanySettings.
        company.CompanySettings = new CompanySettings { Timezone = timezone, PeriodTimeControlMax = 10 };

        // Crear los días de trabajo para la compañía, por defecto días laborables de lunes a viernes.
        company.WorkingDaysWeek = new Domain.Entities.WorkingDaysWeek
        {
            Monday = true,
            Tuesday = true,
            Wednesday = true,
            Thursday = true,
            Friday = true,
            Saturday = false,
            Sunday = false
        };

        await context.Companies.AddAsync(company, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return company;
    }
}
