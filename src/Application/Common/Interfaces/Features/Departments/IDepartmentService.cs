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
    /// Crea un nuevo <see cref="Department" />.
    /// </summary>
    /// <param name="department">Datos del <see cref="Department" />.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <returns><see cref="Department" /> creado.</returns>
    Task<Department> CreateAsync(Department department, CancellationToken cancellationToken);
}
