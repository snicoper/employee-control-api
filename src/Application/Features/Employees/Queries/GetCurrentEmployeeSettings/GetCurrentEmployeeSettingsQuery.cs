using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;

namespace EmployeeControl.Application.Features.Employees.Queries.GetCurrentEmployeeSettings;

[Authorize(Roles = Roles.Employee)]
public record GetCurrentEmployeeSettingsQuery : IQuery<GetCurrentEmployeeSettingsResponse>;
