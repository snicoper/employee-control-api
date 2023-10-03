using MediatR;

namespace EmployeeControl.Application.Features.Identity.Commands.RefreshToken;

public record RefreshTokenCommand(string RefreshToken) : IRequest<RefreshTokenDto>;
