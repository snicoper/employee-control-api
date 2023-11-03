namespace EmployeeControl.Application.Common.Interfaces.Features.Company;

public interface ICompanyService
{
    /// <summary>
    /// Crea una compañía.
    /// </summary>
    /// <param name="company">Datos de la compañía.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <returns>La entidad <see cref="Domain.Entities.Company" /> creada.</returns>
    Task<Domain.Entities.Company> CreateAsync(Domain.Entities.Company company, CancellationToken cancellationToken);
}
