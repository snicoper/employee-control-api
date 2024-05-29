﻿using EmployeeControl.Domain.Entities;
using EmployeeControl.Domain.Enums;
using EmployeeControl.Domain.Exceptions;

namespace EmployeeControl.Domain.Repositories;

public interface ITimeControlRepository
{
    /// <summary>
    /// Obtener un <see cref="TimeControl" /> por su Id.
    /// </summary>
    /// <param name="id">Id del <see cref="TimeControl" />.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <exception cref="NotFoundException">En caso de no encontrar el <see cref="TimeControl" />.</exception>
    /// <returns><see cref="TimeControl" />.</returns>
    Task<TimeControl> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Obtener un <see cref="TimeControl" /> por su Id con datos de <see cref="User" />.
    /// </summary>
    /// <param name="id">Id del <see cref="TimeControl" />.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <exception cref="NotFoundException">En caso de no encontrar el <see cref="TimeControl" />.</exception>
    /// <returns><see cref="TimeControl" />.</returns>
    Task<TimeControl> GetWithEmployeeInfoByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Obtener un <see cref="TimeControl" /> con estado <see cref="TimeState.Open" /> de un empleado.
    /// </summary>
    /// <param name="employeeId">Id del empleado.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <returns><see cref="TimeControl" />.</returns>
    Task<TimeControl?> GetTimeStateOpenByEmployeeIdAsync(Guid employeeId, CancellationToken cancellationToken);

    /// <summary>
    /// Obtener un grupos por días de <see cref="TimeControl" /> en un rango de fechas de un
    /// empleado concreto por su Id.
    /// <para>El rango se busca tanto en Start como Finish del <see cref="TimeControl" />.</para>
    /// </summary>
    /// <param name="employeeId">Id empleado.</param>
    /// <param name="from">Fecha de inicio.</param>
    /// <param name="to">Fecha final.</param>
    /// <param name="cancellationToken">Cancelation token.</param>
    /// <returns>Grupo por días de <see cref="TimeControl" />.</returns>
    Task<IEnumerable<IGrouping<int, TimeControl>>> GetRangeByEmployeeIdAsync(
        Guid employeeId,
        DateTimeOffset from,
        DateTimeOffset to,
        CancellationToken cancellationToken);

    /// <summary>
    /// Obtener los <see cref="TimeControl" />.
    /// <para>Incluye los <see cref="User" />.</para>
    /// </summary>
    /// <returns><see cref="IQueryable{T}" /> con los <see cref="TimeControl" />.</returns>
    IQueryable<TimeControl> GetWithUserQueryable();

    /// <summary>
    /// Obtener los <see cref="TimeControl" /> por el Id de <see cref="User" />.
    /// <para>Incluye los <see cref="User" />.</para>
    /// </summary>
    /// <param name="employeeId">Id <see cref="User" />.</param>
    /// <returns><see cref="IQueryable{T}" /> con los <see cref="TimeControl" />.</returns>
    IQueryable<TimeControl> GetWithUserByEmployeeIdQueryable(Guid employeeId);

    /// <summary>
    /// Obtener si el empleado tiene algún <see cref="TimeControl" /> inicializado.
    /// </summary>
    /// <param name="user"><see cref="User" />.</param>
    /// <param name="cancellationToken">CancellationToken.</param>
    /// <returns>true si tiene el <see cref="TimeControl" /> inicializado, false en caso contrario.</returns>
    Task<TimeState> GetTimeStateByEmployeeAsync(User user, CancellationToken cancellationToken);

    /// <summary>
    /// Obtener los <see cref="TimeControl" /> con incidencias.
    /// </summary>
    /// <returns>Lista de <see cref="TimeControl" /> con incidencias.</returns>
    IQueryable<TimeControl> GetTimesControlIncidencesQueryable();

    /// <summary>
    /// Obtener cantidad de incidencias.
    /// </summary>
    /// <returns>Total de <see cref="TimeControl" /> con incidencias.</returns>
    int CountIncidences();

    /// <summary>
    /// Cerrar una incidencia por el Id de <see cref="TimeControl" />.
    /// </summary>
    /// <param name="id">Id del <see cref="TimeControl" />.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <returns><see cref="TimeControl" />.</returns>
    Task<TimeControl> CloseIncidenceByTimeControlIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Cerrar una incidencia.
    /// </summary>
    /// <param name="timeControl"><see cref="TimeControl" /> a cerrar incidencia.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <returns><see cref="TimeControl" />.</returns>
    Task<TimeControl> CloseIncidenceByTimeControlAsync(TimeControl timeControl, CancellationToken cancellationToken);

    /// <summary>
    /// Crea un <see cref="TimeControl" /> sin hora final.
    /// </summary>
    /// <param name="timeControl"><see cref="TimeControl" /> a crear.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <returns><see cref="TimeControl" /> creado.</returns>
    Task<TimeControl> CreateWithOutFinishAsync(TimeControl timeControl, CancellationToken cancellationToken);

    /// <summary>
    /// Crea un <see cref="TimeControl" /> con su hora de inicio y final.
    /// </summary>
    /// <param name="timeControl"><see cref="TimeControl" /> a crear.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <returns><see cref="TimeControl" /> creado.</returns>
    Task<TimeControl> CreateWithFinishAsync(TimeControl timeControl, CancellationToken cancellationToken);

    /// <summary>
    /// Inicializar un <see cref="TimeControl" />.
    /// <para>Lanza un <see cref="NotFoundException" /> si el empleado no existe.</para>
    /// </summary>
    /// <param name="user"><see cref="User" />.</param>
    /// <param name="deviceType">Dispositivo utilizado.</param>
    /// <param name="latitude">Longitud del empleado.</param>
    /// <param name="longitude">Latitud del empleado.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns><see cref="TimeControl" /> creado.</returns>
    Task<TimeControl> StartAsync(
        User user,
        DeviceType deviceType,
        double? latitude,
        double? longitude,
        CancellationToken cancellationToken);

    /// <summary>
    /// Cierra un <see cref="TimeControl" /> con la hora actual.
    /// </summary>
    /// <param name="user"><see cref="User" />.</param>
    /// <param name="deviceType">Dispositivo utilizado.</param>
    /// <param name="closedBy"><see cref="ClosedBy" />, quien finaliza el tiempo.</param>
    /// <param name="latitude">Longitud del empleado.</param>
    /// <param name="longitude">Latitud del empleado.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    Task<TimeControl> FinishAsync(
        User user,
        DeviceType deviceType,
        ClosedBy closedBy,
        double? latitude,
        double? longitude,
        CancellationToken cancellationToken);

    /// <summary>
    /// Actualiza un <see cref="TimeControl" />.
    /// </summary>
    /// <param name="timeControl"><see cref="TimeControl" /> a actualizar.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <returns><see cref="TimeControl" /> actualizado.</returns>
    Task<TimeControl> UpdateAsync(TimeControl timeControl, CancellationToken cancellationToken);
}
