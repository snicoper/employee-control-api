namespace EmployeeControl.Application.Common.Interfaces.Identity;

public interface ILoginService
{
    /// <summary>
    /// Log in de un usuario.
    /// </summary>
    /// <param name="identifier">Un identificador valido.</param>
    /// <param name="password">Password.</param>
    /// <returns>Jwt en caso de éxito.</returns>
    Task<string> LoginAsync(string identifier, string password);
}
