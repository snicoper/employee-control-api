using MediatR;

namespace EmployeeControl.Application.Cqrs.Identity.Commands.RefreshToken;

public record RefreshTokenCommand(string RefreshToken) : IRequest<RefreshTokenDto>;
