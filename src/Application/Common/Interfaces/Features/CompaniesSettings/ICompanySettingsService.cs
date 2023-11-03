using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Common.Interfaces.Features.CompaniesSettings;

public interface ICompanySettingsService
{
    /// <summary>
    /// Obtener <see cref="CompanySettings" /> por el Id de compañía.
    /// </summary>
    /// <param name="companyId">Id compañía.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <returns><see cref="CompanySettings" /> object.</returns>
    Task<CompanySettings> GetByCompanyIdAsync(string companyId, CancellationToken cancellationToken);

    /// <summary>
    /// Obtener el timezone de una compañía.
    /// <para>
    /// Si la compañía no tiene un timezone, devolverá un <see cref="TimeZoneInfo" />.Local.Id.
    /// </para>
    /// </summary>
    /// <param name="companyId">Id compañía.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <returns>Timezone Iana.</returns>
    Task<string> GetIanaTimezoneCompanyAsync(string companyId, CancellationToken cancellationToken);

    /// <summary>
    /// Convertir un <see cref="DateTimeOffset" /> a la zona horaria según el timezone de la compañía.
    /// </summary>
    /// <param name="datetime"><see cref="DateTimeOffset" />.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <returns><see cref="DateTimeOffset" /> con el timezone de la actual compañía.</returns>
    Task<DateTimeOffset> ConvertToTimezoneCurrentCompanyAsync(DateTimeOffset datetime, CancellationToken cancellationToken);

    /// <summary>
    /// Crea una fila de configuración para una compañía.
    /// </summary>
    /// <param name="companySettings"><see cref="CompanySettings" /> datos.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <returns><see cref="CompanySettings" /> creado.</returns>
    Task<CompanySettings> CreateAsync(CompanySettings companySettings, CancellationToken cancellationToken);
}
