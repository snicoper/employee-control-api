namespace EmployeeControl.Application.Common.Interfaces;

public interface ICompanyService
{
    Task CreateCompany(string name, CancellationToken cancellationToken);
}
