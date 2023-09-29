using MediatR;

namespace EmployeeControl.Application.Identity.Commands.Login;

public record LoginCommand(string UserName, string Password)
    : IRequest<LoginDto>
{
}
