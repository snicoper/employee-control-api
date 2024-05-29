using EmployeeControl.Domain.Common;
using MediatR;

namespace EmployeeControl.Application.Common.Interfaces.Messaging;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
