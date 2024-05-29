using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;

namespace EmployeeControl.Application.Features.Employees.Queries.GetEmployeeById;

[Authorize(Roles = Roles.HumanResources)]
public record GetEmployeeByIdQuery(Guid Id) : IQuery<GetEmployeeByIdResponse>;
