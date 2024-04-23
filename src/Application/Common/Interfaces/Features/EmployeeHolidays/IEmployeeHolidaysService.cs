using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Common.Interfaces.Features.EmployeeHolidays;

public interface IEmployeeHolidaysService
{
    /// <summary>
    /// Obtener <see cref="EmployeeHoliday" /> por el Id del empleado de un año concreto.
    /// </summary>
    /// <param name="year">Año al que obtener el <see cref="EmployeeHoliday" />.</param>
    /// <param name="employeeId">Id del empleado.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <returns><see cref="EmployeeHoliday" />.</returns>
    Task<EmployeeHoliday> GetByYearByEmployeeId(int year, string employeeId, CancellationToken cancellationToken);
}
