﻿using EmployeeControl.Application.Common.Interfaces.Common;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Features.CompanyTask;
using EmployeeControl.Application.Localizations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace EmployeeControl.Infrastructure.Services.Features.CompanyTask;

public class CompanyTaskService(
        IStringLocalizer<CompanyTaskLocalizer> localizer,
        IValidationFailureService validationFailureService,
        IApplicationDbContext context)
    : ICompanyTaskService
{
    public async Task<Domain.Entities.CompanyTask> CreateAsync(
        Domain.Entities.CompanyTask newCompanyTask,
        CancellationToken cancellationToken)
    {
        var companyTaskExists = context
            .CompanyTasks
            .AsNoTracking()
            .Where(ct => ct.CompanyId == newCompanyTask.CompanyId && ct.Name == newCompanyTask.Name);

        if (companyTaskExists.Any())
        {
            var message = localizer["El nombre de compañía ya existe."];
            validationFailureService.AddAndRaiseException(nameof(Domain.Entities.CompanyTask.Name), message);
        }

        newCompanyTask.Active = true;

        await context.CompanyTasks.AddAsync(newCompanyTask, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return newCompanyTask;
    }
}
