namespace EmployeeControl.Application.Features.Auth.Commands.Login;

public record LoginDto(string? AccessToken, string? RefreshToken);
