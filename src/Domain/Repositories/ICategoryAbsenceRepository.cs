﻿using EmployeeControl.Domain.Entities;
using EmployeeControl.Domain.Exceptions;

namespace EmployeeControl.Domain.Repositories;

public interface ICategoryAbsenceRepository
{
    /// <summary>
    /// Obtener un <see cref="CategoryAbsence" /> por su Id.
    /// <para>
    /// Si no existe el <see cref="CategoryAbsence" /> lanza un <see cref="NotFoundException" />.
    /// </para>
    /// </summary>
    /// <param name="id">Id de <see cref="CategoryAbsence" />.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <returns><see cref="CategoryAbsence" /> en caso de existir.</returns>
    Task<CategoryAbsence> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Creación de una nueva <see cref="CategoryAbsence" />.
    /// <para>
    /// </para>
    /// </summary>
    /// <param name="categoryAbsence">Datos para crear <see cref="CategoryAbsence" />.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <returns><see cref="CategoryAbsence" /> creado.</returns>
    Task<CategoryAbsence> CreateAsync(CategoryAbsence categoryAbsence, CancellationToken cancellationToken);

    /// <summary>
    /// Actualizar un <see cref="CategoryAbsence" />.
    /// </summary>
    /// <param name="categoryAbsence">Datos de <see cref="CategoryAbsence" />.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <returns>El <see cref="CategoryAbsence" /> actualizado.</returns>
    Task<CategoryAbsence> UpdateAsync(CategoryAbsence categoryAbsence, CancellationToken cancellationToken);
}
