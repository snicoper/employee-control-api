using EmployeeControl.Application.Common.Interfaces.Messaging;

namespace EmployeeControl.Application.Features.Auth.Commands.RefreshToken;

public record RefreshTokenCommand(string RefreshToken) : ICommand<RefreshTokenResponse>;
