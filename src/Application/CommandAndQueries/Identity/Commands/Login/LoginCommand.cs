using MediatR;

namespace EmployeeControl.Application.CommandAndQueries.Identity.Commands.Login;

public record LoginCommand(string UserName, string Password)
    : IRequest<LoginDto>
{
}
