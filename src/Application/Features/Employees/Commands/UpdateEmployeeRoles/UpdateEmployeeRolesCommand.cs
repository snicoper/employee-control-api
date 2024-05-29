using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;

namespace EmployeeControl.Application.Features.Employees.Commands.UpdateEmployeeRoles;

[Authorize(Roles = Roles.Employee)]
public record UpdateEmployeeRolesCommand(Guid EmployeeId, List<string> RolesToAdd) : ICommand;
