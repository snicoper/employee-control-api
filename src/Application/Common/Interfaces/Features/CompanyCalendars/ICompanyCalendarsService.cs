using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Common.Interfaces.Features.CompanyCalendars;

public interface ICompanyCalendarsService
{
    /// <summary>
    /// Obtener lista de <see cref="CompanyCalendar" />.
    /// </summary>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <returns>Lista de <see cref="CompanyCalendar" />.</returns>
    Task<ICollection<CompanyCalendar>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Obtener un <see cref="CompanyCalendar" /> por su Id.
    /// </summary>
    /// <param name="id">Id del <see cref="CompanyCalendar" />.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <returns><see cref="CompanyCalendar" />.</returns>
    Task<CompanyCalendar> GetByIdAsync(string id, CancellationToken cancellationToken);
}
