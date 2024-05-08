﻿using EmployeeControl.Application.Common.Exceptions;
using EmployeeControl.Application.Common.Interfaces.Common;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Features.CompanyTask;
using EmployeeControl.Application.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace EmployeeControl.Infrastructure.Services.Features.CompanyTask;

public class CompanyTaskService(
    IStringLocalizer<TaskResource> localizer,
    IValidationFailureService validationFailureService,
    IApplicationDbContext context)
    : ICompanyTaskService
{
    public async Task<Domain.Entities.CompanyTask> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var result = await context
                         .CompanyTasks
                         .SingleOrDefaultAsync(cs => cs.Id == id, cancellationToken) ??
                     throw new NotFoundException(nameof(Domain.Entities.CompanyTask), nameof(Domain.Entities.CompanyTask.Id));

        return result;
    }

    public async Task<Domain.Entities.CompanyTask> CreateAsync(
        Domain.Entities.CompanyTask newCompanyTask,
        CancellationToken cancellationToken)
    {
        var companyTaskExists = context
            .CompanyTasks
            .Where(ct => ct.Name == newCompanyTask.Name);

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
