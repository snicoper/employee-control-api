using MediatR;

namespace EmployeeControl.Application.Cqrs.Identity.Commands.Login;

public record LoginCommand(string Identifier, string Password) : IRequest<LoginDto>;
