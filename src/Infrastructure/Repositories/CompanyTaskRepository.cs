using EmployeeControl.Application.Common.Extensions;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Localization;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Entities;
using EmployeeControl.Domain.Exceptions;
using EmployeeControl.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace EmployeeControl.Infrastructure.Repositories;

public class CompanyTaskRepository(IStringLocalizer<TaskResource> localizer, IApplicationDbContext context)
    : ICompanyTaskRepository
{
    public async Task<CompanyTask> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var result = await context
                         .CompanyTasks
                         .SingleOrDefaultAsync(cs => cs.Id == id, cancellationToken)
                     ?? throw new NotFoundException(nameof(CompanyTask), nameof(CompanyTask.Id));

        return result;
    }

    public async Task<CompanyTask> CreateAsync(
        CompanyTask newCompanyTask,
        CancellationToken cancellationToken)
    {
        var companyTaskExists = context
            .CompanyTasks
            .Where(ct => ct.Name == newCompanyTask.Name);

        if (companyTaskExists.Any())
        {
            var message = localizer["El nombre de compañía ya existe."];
            Result.Failure(nameof(CompanyTask.Name), message).RaiseBadRequest();
        }

        newCompanyTask.Active = true;

        await context.CompanyTasks.AddAsync(newCompanyTask, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return newCompanyTask;
    }
}
