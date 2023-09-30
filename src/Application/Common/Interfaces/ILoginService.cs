namespace EmployeeControl.Application.Common.Interfaces;

public interface ILoginService
{
    Task<string> LoginAsync(string identifier, string password);
}
