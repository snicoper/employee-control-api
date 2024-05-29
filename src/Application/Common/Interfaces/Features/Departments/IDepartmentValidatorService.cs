using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Common.Interfaces.Features.Departments;

public interface IDepartmentValidatorService
{
    /// <summary>
    /// Valida el nombre de un <see cref="Department" />.
    /// <para>Un nombre no puede ser repetido en una misma empresa.</para>
    /// <para>Valida para actualización de <see cref="Department" />.</para>
    /// </summary>
    /// <param name="department">Datos <see cref="Department" />.</param>
    /// <param name="result"><see cref="Result" /> para setear posibles errores.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    Task<Result> ValidateNameAsync(Department department, Result result, CancellationToken cancellationToken);

    /// <summary>
    /// Valida el <see cref="Department.Background" /> y <see cref="Department.Color" />
    /// del <see cref="Department" /> ya que solo puede haber una repetición por compañía.
    /// <para>Valida para actualización de <see cref="Department" />.</para>
    /// </summary>
    /// <param name="department">Datos <see cref="Department" />.</param>
    /// <param name="result"><see cref="Result" /> para setear posibles errores.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    Task<Result> ValidateBackgroundAndColorAsync(Department department, Result result, CancellationToken cancellationToken);
}
