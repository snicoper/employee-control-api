using MediatR;

namespace EmployeeControl.Application.Identity.Commands.CreateAccount;

public class CreateAccountHandler : IRequestHandler<CreateAccountCommand, CreateAccountDto>
{
    public Task<CreateAccountDto> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
