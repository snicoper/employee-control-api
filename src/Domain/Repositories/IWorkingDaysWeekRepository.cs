﻿using EmployeeControl.Domain.Entities;
using EmployeeControl.Domain.Exceptions;

namespace EmployeeControl.Domain.Repositories;

public interface IWorkingDaysWeekRepository
{
    /// <summary>
    /// Obtener un <see cref="WorkingDaysWeek" /> por el Id de la <see cref="Company" />.
    /// </summary>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <exception cref="NotFoundException">En caso de no existir.</exception>
    /// <returns><see cref="WorkingDaysWeek" /> en caso de existir.</returns>
    Task<WorkingDaysWeek> GetWorkingDaysWeekAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Actualizar un <see cref="WorkingDaysWeek" />.
    /// </summary>
    /// <param name="workingDaysWeek">Datos a actualizar.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <returns><see cref="WorkingDaysWeek" /> actualizado.</returns>
    Task<WorkingDaysWeek> UpdateAsync(
        WorkingDaysWeek workingDaysWeek,
        CancellationToken cancellationToken);
}
