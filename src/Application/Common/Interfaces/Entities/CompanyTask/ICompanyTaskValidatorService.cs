namespace EmployeeControl.Application.Common.Interfaces.Entities.CompanyTask;

public interface ICompanyTaskValidatorService
{
    /// <summary>
    /// Comprueba si la compañía ya tiene una tarea con el mismo nombre.
    /// </summary>
    /// <param name="companyTask">Datos de la tarea.</param>
    void ValidateCompanyName(Domain.Entities.CompanyTask companyTask);
}
