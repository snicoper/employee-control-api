using EmployeeControl.Application.Common.Models;
using MediatR;

namespace EmployeeControl.Application.Features.Employee.Commands.InviteEmployee;

public class InviteEmployeeHandler : IRequestHandler<InviteEmployeeCommand, Result>
{
    public Task<Result> Handle(InviteEmployeeCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
