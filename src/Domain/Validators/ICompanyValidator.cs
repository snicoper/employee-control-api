using EmployeeControl.Domain.Common;

namespace EmployeeControl.Domain.Validators;

public interface ICompanyValidator
{
    Task<Result> UniqueNameValidationAsync(string companyName, Result result, CancellationToken cancellationToken);
}
