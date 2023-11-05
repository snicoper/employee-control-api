using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Common.Interfaces.Features.Departments;

public interface IDepartmentService
{
    /// <summary>
    /// Crea un nuevo <see cref="Department" />.
    /// </summary>
    /// <param name="department">Datos del <see cref="Department" />.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <returns><see cref="Department" /> creado.</returns>
    Task<Department> CreateAsync(Department department, CancellationToken cancellationToken);
}
