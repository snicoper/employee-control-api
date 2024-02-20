using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Common.Interfaces.Features.Identity;

public interface IEmployeeSettingsService
{
    /// <summary>
    /// Crea configuración de un usuario.
    /// </summary>
    /// <param name="employeeSettings"><see cref="EmployeeSettings" />.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    Task<int> CreateAsync(EmployeeSettings employeeSettings, CancellationToken cancellationToken);
}
