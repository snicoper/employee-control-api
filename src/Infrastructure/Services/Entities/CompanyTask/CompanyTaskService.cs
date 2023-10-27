using EmployeeControl.Application.Common.Interfaces.Common;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Entities.CompanyTask;

namespace EmployeeControl.Infrastructure.Services.Entities.CompanyTask;

public class CompanyTaskService(
        ICompanyTaskValidatorService companyTaskValidatorService,
        IValidationFailureService validationFailureService,
        IApplicationDbContext context)
    : ICompanyTaskService
{
    public async Task<Domain.Entities.CompanyTask> CreateAsync(
        Domain.Entities.CompanyTask newCompanyTask,
        CancellationToken cancellationToken)
    {
        companyTaskValidatorService.ValidateCompanyName(newCompanyTask);

        validationFailureService.RaiseExceptionIfExistsErrors();

        newCompanyTask.Active = true;

        await context.CompanyTasks.AddAsync(newCompanyTask, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return newCompanyTask;
    }
}
