using EmployeeControl.Application.Common.Interfaces.Common;
using EmployeeControl.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace EmployeeControl.Application.Common.Interfaces.Features.Identity;

public interface IIdentityValidatorService
{
    /// <summary>
    /// Comprueba si el Email esta en uso.
    /// <para>Si esta en uso, añade un error en <see cref="IValidationFailureService" />.</para>
    /// </summary>
    /// <param name="user">Usuario con el email a validar.</param>
    /// <param name="cancellationToken">CancellationToken.</param>
    Task UniqueEmailValidationAsync(User user, CancellationToken cancellationToken);

    /// <summary>
    /// Validación de usuario de <see cref="IUserValidator{TUser}" />.
    /// <para>Si esta en uso, añade un error en <see cref="IValidationFailureService" />.</para>
    /// </summary>
    /// <param name="user">Usuario con el email a validar.</param>
    Task UserValidationAsync(User user);

    /// <summary>
    /// Validación del password con <see cref="IPasswordValidator{TUser}" />.
    /// <para>Si esta en uso, añade un error en <see cref="IValidationFailureService" />.</para>
    /// </summary>
    /// <param name="user">Usuario a validar.</param>
    /// <param name="password">Password a validar.</param>
    Task PasswordValidationAsync(User user, string password);

    /// <summary>
    /// Validaciones para asignar roles a un <see cref="User" />.
    /// </summary>
    /// <param name="user"><see cref="User" />.</param>
    /// <param name="userRoles">Roles a asigna al <see cref="User" />.</param>
    void ValidateUpdateEmployeeRoles(User user, IEnumerable<string> userRoles);
}
