﻿using EmployeeControl.Application.Common.Extensions;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Entities;
using EmployeeControl.Domain.Exceptions;
using EmployeeControl.Domain.Repositories;
using EmployeeControl.Domain.Validators;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl.Infrastructure.Repositories;

public class CompanyRepository(IApplicationDbContext context, ICompanyValidator companyValidator)
    : ICompanyRepository
{
    public async Task<Company> GetCompanyAsync(CancellationToken cancellationToken)
    {
        var result = await context
                .Companies
                .FirstOrDefaultAsync(cancellationToken)
            ?? throw new NotFoundException(nameof(Company), nameof(Company));

        return result;
    }

    public async Task<Company> GetCompanyByIdAsync(Guid companyId, CancellationToken cancellationToken)
    {
        var result = await context
                .Companies
                .SingleOrDefaultAsync(c => c.Id == companyId, cancellationToken)
            ?? throw new NotFoundException(nameof(Company), nameof(Company));

        return result;
    }

    public async Task<Company> CreateAsync(Company company, string timezone, CancellationToken cancellationToken)
    {
        // Validaciones.
        var result = Result.Create();
        result = await companyValidator.UniqueNameValidationAsync(company.Name, result, cancellationToken);
        result.RaiseBadRequestIfErrorsExist();

        company.CompanySettings = new CompanySettings { Timezone = timezone, PeriodTimeControlMax = 10 };

        company.WorkingDaysWeek = new WorkingDaysWeek
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
