using EmployeeControl.Domain.Entities;
using EmployeeControl.Domain.Exceptions;

namespace EmployeeControl.Domain.Repositories;

public interface IDepartmentRepository
{
    /// <summary>
    /// Obtener departamentos de la compañía.
    /// </summary>
    /// <returns><see cref="IQueryable" /> de la consulta.</returns>
    IQueryable<Department> GetAllQueryable();

    /// <summary>
    /// Obtener departamentos por el Id del <see cref="User" />.
    /// </summary>
    /// <param name="employeeId">Id empleado.</param>
    /// <returns><see cref="IQueryable{T}" /> de la consulta.</returns>
    IQueryable<Department> GetAllByEmployeeIdQueryable(Guid employeeId);

    /// <summary>
    /// Obtener un <see cref="Department" /> por su Id.
    /// </summary>
    /// <param name="departmentId">Id departamento.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <exception cref="NotFoundException"> en caso de no existir.</exception>
    /// <returns><see cref="Department" />.</returns>
    Task<Department> GetByIdAsync(Guid departmentId, CancellationToken cancellationToken);

    /// <summary>
    /// Crea un nuevo <see cref="Department" />.
    /// </summary>
    /// <param name="department">Datos del <see cref="Department" />.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <returns><see cref="Department" /> creado.</returns>
    Task<Department> CreateAsync(Department department, CancellationToken cancellationToken);

    /// <summary>
    /// Actualizar un <see cref="Department" />.
    /// </summary>
    /// <param name="department"><see cref="Department" />.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <returns><see cref="Department" /> creado.</returns>
    Task<Department> UpdateAsync(Department department, CancellationToken cancellationToken);
}
