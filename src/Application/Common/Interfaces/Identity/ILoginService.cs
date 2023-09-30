namespace EmployeeControl.Application.Common.Interfaces.Identity;

public interface ILoginService
{
    Task<string> LoginAsync(string identifier, string password);
}
