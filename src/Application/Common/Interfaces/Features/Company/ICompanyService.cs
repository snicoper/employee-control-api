namespace EmployeeControl.Application.Common.Interfaces.Features.Company;

public interface ICompanyService
{
    Task<Domain.Entities.Company> CreateCompanyAsync(Domain.Entities.Company company, CancellationToken cancellationToken);
}
