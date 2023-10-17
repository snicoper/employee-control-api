namespace EmployeeControl.Application.Common.Interfaces.Entities.Company;

public interface ICompanyService
{
    Task<Domain.Entities.Company> CreateCompanyAsync(Domain.Entities.Company company, CancellationToken cancellationToken);
}
