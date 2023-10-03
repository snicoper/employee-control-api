namespace EmployeeControl.Application.Cqrs.Identity.Commands.Login;

public class LoginDto
{
    public string? AccessToken { get; set; }

    public string? RefreshToken { get; set; }
}
