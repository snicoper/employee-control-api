namespace EmployeeControl.Application.Features.Auth.Commands.Login;

public class LoginDto
{
    public string? AccessToken { get; set; }

    public string? RefreshToken { get; set; }
}
