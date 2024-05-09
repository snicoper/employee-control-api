using EmployeeControl.Application.Common.Models;
using MediatR;

namespace EmployeeControl.Application.Common.Interfaces.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
