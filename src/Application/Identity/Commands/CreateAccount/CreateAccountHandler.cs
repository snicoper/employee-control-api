using MediatR;

namespace EmployeeControl.Application.Identity.Commands.CreateAccount;

public class CreateAccountHandler : IRequestHandler<CreateAccountCommand, int>
{
    public Task<int> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
