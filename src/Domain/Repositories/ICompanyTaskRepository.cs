using EmployeeControl.Domain.Entities;
using EmployeeControl.Domain.Exceptions;

namespace EmployeeControl.Domain.Repositories;

public interface ICompanyTaskRepository
{
    /// <summary>
    /// Obtener una <see cref="Domain.Entities.CompanyTask" /> por du Id.
    /// </summary>
    /// <param name="id">Id de la <see cref="Domain.Entities.CompanyTask" />.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <exception cref="NotFoundException">Si no existe en la base de datos.</exception>
    /// <returns><see cref="Domain.Entities.CompanyTask" />.</returns>
    Task<CompanyTask> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Crea una nueva <see cref="Domain.Entities.CompanyTask" />.
    /// </summary>
    /// <param name="newCompanyTask"><see cref="Domain.Entities.CompanyTask" />.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <returns><see cref="Domain.Entities.CompanyTask" />Creada.</returns>
    Task<CompanyTask> CreateAsync(
        CompanyTask newCompanyTask,
        CancellationToken cancellationToken);
}
