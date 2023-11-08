using EmployeeControl.Application.Common.Exceptions;
using EmployeeControl.Application.Common.Interfaces.Common;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Entities;
using EmployeeControl.Domain.Enums;

namespace EmployeeControl.Application.Common.Interfaces.Features.TimesControl;

public interface ITimesControlService
{
    /// <summary>
    /// Obtener un grupos por días de <see cref="TimeControl" /> en un rango de fechas de un
    /// empleado concreto por su Id.
    /// </summary>
    /// <param name="employeeId">Id empleado.</param>
    /// <param name="from">Fecha de inicio.</param>
    /// <param name="to">Fecha final.</param>
    /// <param name="cancellationToken">Cancelation token.</param>
    /// <returns>Grupo por días de <see cref="TimeControl" />.</returns>
    Task<IEnumerable<IGrouping<int, TimeControl>>> GetRangeByEmployeeIdAsync(
        string employeeId,
        DateTimeOffset from,
        DateTimeOffset to,
        CancellationToken cancellationToken);

    /// <summary>
    /// Obtener los <see cref="TimeControl" /> por el Id de <see cref="Company" />.
    /// <para>Incluye los <see cref="ApplicationUser" />.</para>
    /// </summary>
    /// <param name="companyId">Id <see cref="Company" />.</param>
    /// <returns><see cref="IQueryable{T}" /> con los <see cref="TimeControl" />.</returns>
    IQueryable<TimeControl> GetWithUserByCompanyId(string companyId);

    /// <summary>
    /// Obtener los <see cref="TimeControl" /> por el Id de <see cref="ApplicationUser" />.
    /// <para>Incluye los <see cref="ApplicationUser" />.</para>
    /// </summary>
    /// <param name="employeeId">Id <see cref="ApplicationUser" />.</param>
    /// <returns><see cref="IQueryable{T}" /> con los <see cref="TimeControl" />.</returns>
    IQueryable<TimeControl> GetWithUserByEmployeeId(string employeeId);

    /// <summary>
    /// Obtener si el empleado tiene algún <see cref="TimeControl" /> inicializado.
    /// </summary>
    /// <param name="employeeId">Id empleado.</param>
    /// <param name="cancellationToken">CancellationToken.</param>
    /// <returns>true si tiene el <see cref="TimeControl" /> inicializado, false en caso contrario.</returns>
    Task<TimeState> GetTimeStateByEmployeeIAsync(string employeeId, CancellationToken cancellationToken);

    /// <summary>
    /// Inicializar un <see cref="TimeControl" />.
    /// <para>Lanza un <see cref="NotFoundException" /> si el empleado no existe.</para>
    /// <para>Lanza un <see cref="IValidationFailureService" /> si ya tenía un tiempo inicializado.</para>
    /// </summary>
    /// <param name="employeeId">Id del empleado.</param>
    /// <param name="deviceType">Dispositivo utilizado.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Result con la respuesta y <see cref="TimeControl" /> creado.</returns>
    Task<(Result Result, TimeControl TimeControl)> StartAsync(
        string employeeId,
        DeviceType deviceType,
        CancellationToken cancellationToken);

    /// <summary>
    /// Inicia un TimeControl.
    /// <para>Lanza un <see cref="IValidationFailureService" /> en caso de error.</para>
    /// </summary>
    /// <param name="employeeId">Id empleado.</param>
    /// <param name="deviceType">Dispositivo utilizado.</param>
    /// <param name="closedBy"><see cref="ClosedBy" />, quien finaliza el tiempo.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <returns>Result.Success en caso de exito, <see cref="IValidationFailureService" /> en caso contrario.</returns>
    Task<(Result Result, TimeControl? TimeControl)> FinishAsync(
        string employeeId,
        DeviceType deviceType,
        ClosedBy closedBy,
        CancellationToken cancellationToken);
}
