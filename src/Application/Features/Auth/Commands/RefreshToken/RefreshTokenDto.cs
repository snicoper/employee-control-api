namespace EmployeeControl.Application.Features.Auth.Commands.RefreshToken;

public record RefreshTokenDto(string? AccessToken, string? RefreshToken);
