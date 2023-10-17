namespace EmployeeControl.Application.Common.Interfaces.Entities.Company;

public interface ICompanyValidatorService
{
    Task UniqueNameValidationAsync(string company, CancellationToken cancellationToken);
}
