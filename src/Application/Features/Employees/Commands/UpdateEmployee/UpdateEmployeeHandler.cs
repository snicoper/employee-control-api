using EmployeeControl.Application.Common.Models;
using MediatR;

namespace EmployeeControl.Application.Features.Employees.Commands.UpdateEmployee;

internal class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeCommand, Result>
{
    public Task<Result> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
