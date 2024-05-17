using EmployeeControl.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace EmployeeControl.Domain.Repositories;

public interface IUserRoleRepository
{
    /// <summary>
    /// Obtener lista de roles de un usuario concreto.
    /// </summary>
    /// <param name="user">Usuario al que se quiere obtener sus roles.</param>
    /// <returns>Lista de <see cref="IdentityRole" />.</returns>
    Task<List<ApplicationRole>> GetRolesByUseAsync(User user);
}
