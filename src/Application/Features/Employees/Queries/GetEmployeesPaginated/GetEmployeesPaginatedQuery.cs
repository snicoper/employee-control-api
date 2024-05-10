using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;

namespace EmployeeControl.Application.Features.Employees.Queries.GetEmployeesPaginated;

[Authorize(Roles = Roles.HumanResources)]
public record GetEmployeesPaginatedQuery(RequestData RequestData) : IQuery<ResponseData<GetEmployeesPaginatedResponse>>;
