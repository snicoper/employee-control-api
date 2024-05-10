using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;

namespace EmployeeControl.Application.Features.Employees.Queries.GetCurrentEmployee;

[Authorize(Roles = Roles.Employee)]
public record GetCurrentEmployeeQuery : IQuery<GetCurrentEmployeeResponse>;
