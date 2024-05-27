using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Common.Interfaces.Features.EmployeeHolidays;

public interface IEmployeeHolidaysService
{
    /// <summary>
    ///     Obtiene un <see cref="EmployeeHoliday" /> dado el id del <see cref="User" /> y el año.
    /// </summary>
    /// <param name="year">Año del <see cref="EmployeeHoliday" />.</param>
    /// <param name="employeeId">Id del <see cref="User" />.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <returns>El <see cref="EmployeeHoliday" />.</returns>
    Task<EmployeeHoliday> GetByEmployeeIdAndYearAsync(
        int year,
        string employeeId,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Comprueba si existe un <see cref="EmployeeHoliday" /> con el id dado.
    /// </summary>
    /// <param name="year">Año del <see cref="EmployeeHoliday" />.</param>
    /// <param name="employeeId">Id del <see cref="User" />.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <returns>True si existe, false en caso contrario.</returns>
    Task<bool> ExistsByYearAndEmployeeId(int year, string employeeId, CancellationToken cancellationToken);

    /// <summary>
    ///     Crea un nuevo <see cref="EmployeeHoliday" />.
    /// </summary>
    /// <param name="year">Año del <see cref="EmployeeHoliday" />.</param>
    /// <param name="employeeId">Id del <see cref="User" />.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <returns>El nuevo <see cref="EmployeeHoliday" />.</returns>
    Task<EmployeeHoliday> CreateAsync(int year, string employeeId, CancellationToken cancellationToken);
}
