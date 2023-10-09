namespace EmployeeControl.Application.Features.Auth.Commands.RefreshToken;

public class RefreshTokenDto
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
}
