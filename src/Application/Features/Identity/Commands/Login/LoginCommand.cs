using MediatR;

namespace EmployeeControl.Application.Features.Identity.Commands.Login;

public record LoginCommand(string Identifier, string Password) : IRequest<LoginDto>;
