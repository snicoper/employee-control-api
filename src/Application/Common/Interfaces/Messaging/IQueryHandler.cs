using EmployeeControl.Domain.Common;
using MediatR;

namespace EmployeeControl.Application.Common.Interfaces.Messaging;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}
