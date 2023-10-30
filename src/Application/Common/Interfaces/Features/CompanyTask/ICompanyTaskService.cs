namespace EmployeeControl.Application.Common.Interfaces.Features.CompanyTask;

public interface ICompanyTaskService
{
    Task<Domain.Entities.CompanyTask> CreateAsync(
        Domain.Entities.CompanyTask newCompanyTask,
        CancellationToken cancellationToken);
}
