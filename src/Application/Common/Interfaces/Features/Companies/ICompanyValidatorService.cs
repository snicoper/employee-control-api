using EmployeeControl.Application.Common.Models;

namespace EmployeeControl.Application.Common.Interfaces.Features.Companies;

public interface ICompanyValidatorService
{
    Task<Result> UniqueNameValidationAsync(string companyName, Result result, CancellationToken cancellationToken);
}
