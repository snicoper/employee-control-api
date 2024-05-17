using EmployeeControl.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace EmployeeControl.Domain.Repositories;

public interface IUserRepository
{
    /// <summary>
    /// Obtener el nombre de un usuario por su Id.
    /// </summary>
    /// <param name="userId">Id usuario.</param>
    /// <returns>Nombre del usuario.</returns>
    Task<string?> GetUserNameAsync(string userId);

    /// <summary>
    /// Obtener usuario por su Id.
    /// </summary>
    /// <param name="userId">Id del usuario.</param>
    /// <returns><see cref="User" />.</returns>
    Task<User> GetByIdAsync(string userId);

    /// <summary>
    /// Obtener <see cref="User" /> con <see cref="CompanyCalendar" /> por el Id del usuario.
    /// </summary>
    /// <param name="userId">Id del usuario a obtener.</param>
    /// <returns><see cref="User" />.</returns>
    Task<User> GetByIdWithCompanyCalendarAsync(string userId);

    /// <summary>
    /// Obtener usuario actual.
    /// </summary>
    /// <returns><see cref="User" />.</returns>
    Task<User> GetCurrentAsync();

    /// <summary>
    /// Obtener un usuario por su Email.
    /// </summary>
    /// <param name="email">Email del usuaio.</param>
    /// <returns><see cref="User" />.</returns>
    Task<User> GetByEmailAsync(string email);

    /// <summary>
    /// Comprueba si un <see cref="User" /> tiene un role asignado.
    /// </summary>
    /// <param name="userId">Id de usuario.</param>
    /// <param name="role">Role a verificar.</param>
    /// <returns>True si tiene el role asignado, false en caso contrario.</returns>
    Task<bool> IsInRoleAsync(string userId, string role);

    Task<bool> AuthorizeAsync(string userId, string policyName);

    /// <summary>
    /// Obtener un <see cref="IQueryable" /> de <see cref="User" />.
    /// </summary>
    /// <returns><see cref="IQueryable" /> de <see cref="User" />.</returns>
    IQueryable<User> GetAllQueryable();

    /// <summary>
    /// Crear un usuario.
    /// <para>Hace las validaciones necesarias para ver si puede ser creado.</para>
    /// </summary>
    /// <param name="user"><see cref="User" />.</param>
    /// <param name="password">Contraseña del usuario.</param>
    /// <param name="roles">Roles a asignar al usuario.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <returns><see cref="User" /> User creado.</returns>
    Task<(IdentityResult IdentityResult, User User)> CreateAsync(
        User user,
        string password,
        IEnumerable<string> roles,
        CancellationToken cancellationToken);

    /// <summary>
    /// Actualizar un usuario.
    /// <para>Hace las validaciones necesarias para ver si puede ser actualizado.</para>
    /// </summary>
    /// <param name="user"><see cref="User" />.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <returns><see cref="IdentityResult" />.</returns>
    Task<(IdentityResult IdentityResult, User User)> UpdateAsync(User user, CancellationToken cancellationToken);

    /// <summary>
    /// Elimina de la base de datos un usuario.
    /// </summary>
    /// <param name="user"><see cref="User" />.</param>
    /// <returns><see cref="IdentityResult" />.</returns>
    Task<IdentityResult> DeleteAsync(User user);

    /// <summary>
    /// Modifica los roles de un usuario.
    /// <para>Elimina todos los roles que tenga actualmente y añade solo los que obtiene.</para>
    /// </summary>
    /// <param name="user"><see cref="User" />.</param>
    /// <param name="rolesToAdd">Roles a añadir.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <returns><see cref="IdentityResult" />.</returns>
    Task<IdentityResult> UpdateRolesByUserIdAsync(
        User user,
        List<string> rolesToAdd,
        CancellationToken cancellationToken);
}
