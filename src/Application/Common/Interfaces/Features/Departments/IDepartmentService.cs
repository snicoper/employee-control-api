using EmployeeControl.Application.Common.Exceptions;
using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Common.Interfaces.Features.Departments;

public interface IDepartmentService
{
    /// <summary>
    /// Obtener departamentos de una compañía por el Id de la compañía.
    /// </summary>
    /// <param name="companyId">Id compañía.</param>
    /// <returns><see cref="IQueryable" /> de la consulta.</returns>
    IQueryable<Department> GetAllByCompanyId(string companyId);

    /// <summary>
    /// Obtener un <see cref="Department" /> por su Id.
    /// </summary>
    /// <param name="departmentId">Id departamento.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <exception cref="NotFoundException"> en caso de no existir.</exception>
    /// <returns><see cref="Department" />.</returns>
    Task<Department> GetByIdAsync(string departmentId, CancellationToken cancellationToken);

    /// <summary>
    /// Crea un nuevo <see cref="Department" />.
    /// </summary>
    /// <param name="department">Datos del <see cref="Department" />.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <returns><see cref="Department" /> creado.</returns>
    Task<Department> CreateAsync(Department department, CancellationToken cancellationToken);
}
