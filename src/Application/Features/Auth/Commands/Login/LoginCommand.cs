using MediatR;

namespace EmployeeControl.Application.Features.Auth.Commands.Login;

public record LoginCommand(string Identifier, string Password) : IRequest<LoginDto>;
