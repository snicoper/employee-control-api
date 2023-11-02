using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Common.Interfaces.Features.CompaniesSettings;

public interface ICompanySettingsService
{
    /// <summary>
    /// Crea una fila de configuración para una compañía.
    /// </summary>
    /// <param name="companySettings"><see cref="CompanySettings" /> datos.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <returns><see cref="CompanySettings" /> creado.</returns>
    Task<CompanySettings> CreateAsync(CompanySettings companySettings, CancellationToken cancellationToken);
}
