using MediatR;

namespace EmployeeControl.Application.Cqrs.Identity.Commands.Login;

public record LoginCommand(string UserName, string Password) : IRequest<LoginDto>;
