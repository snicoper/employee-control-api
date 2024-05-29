using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace EmployeeControl.Application.Common.Interfaces.Features.Identity;

public interface IIdentityValidatorService
{
    /// <summary>
    /// Comprueba si el Email esta en uso.
    /// </summary>
    /// <param name="user">Usuario con el email a validar.</param>
    /// <param name="result"><see cref="Result" />.</param>
    /// <param name="cancellationToken">CancellationToken.</param>
    Task<Result> UniqueEmailValidationAsync(User user, Result result, CancellationToken cancellationToken);

    /// <summary>
    /// Validación de usuario de <see cref="IUserValidator{TUser}" />.
    /// </summary>
    /// <param name="user">Usuario con el email a validar.</param>
    /// <param name="result"><see cref="Result" />.</param>
    Task<Result> UserValidationAsync(User user, Result result);

    /// <summary>
    /// Validación del password con <see cref="IPasswordValidator{TUser}" />.
    /// </summary>
    /// <param name="user">Usuario a validar.</param>
    /// <param name="password">Password a validar.</param>
    /// <param name="result"><see cref="Result" />.</param>
    Task<Result> PasswordValidationAsync(User user, string password, Result result);

    /// <summary>
    /// Validaciones para asignar roles a un <see cref="User" />.
    /// </summary>
    /// <param name="user"><see cref="User" />.</param>
    /// <param name="userRoles">Roles a asigna al <see cref="User" />.</param>
    /// <param name="result"><see cref="Result" />.</param>
    Result ValidateUpdateEmployeeRoles(User user, IEnumerable<string> userRoles, Result result);
}
