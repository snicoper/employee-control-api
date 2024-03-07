using EmployeeControl.Application.Common.Interfaces.Common;
using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Common.Interfaces.Features.CategoryAbsences;

public interface ICategoryAbsenceService
{
    /// <summary>
    /// Creación de una nueva <see cref="CategoryAbsence" />.
    /// <para>
    /// Lanza un <see cref="IValidationFailureService" /> si no cumple las reglas de creación.
    /// </para>
    /// </summary>
    /// <param name="categoryAbsence">Datos para crear <see cref="CategoryAbsence" />.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <returns><see cref="CategoryAbsence" /> creado.</returns>
    Task<CategoryAbsence> CreateAsync(CategoryAbsence categoryAbsence, CancellationToken cancellationToken);
}
