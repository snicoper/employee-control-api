namespace EmployeeControl.Application.Common.Interfaces.Identity;

public interface IAuthService
{
    /// <summary>
    /// Log in de un usuario.
    /// </summary>
    /// <param name="identifier">Un identificador valido.</param>
    /// <param name="password">Password.</param>
    /// <returns>Jwt en caso de éxito.</returns>
    Task<(string AccessToken, string RefreshToken)> LoginAsync(string identifier, string password);

    /// <summary>
    /// Obtener un nuevo Refresh token.
    /// </summary>
    /// <param name="refreshToken">Actual refresh token</param>
    /// <returns>Nuevo token y refresh token en caso de éxito.</returns>
    Task<(string AccessToken, string RefreshToken)> RefreshTokenAsync(string refreshToken);
}
