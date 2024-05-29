using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;

namespace EmployeeControl.Application.Features.Employees.Commands.RemoveRoleHumanResources;

[Authorize(Roles = Roles.Admin)]
public record RemoveRoleHumanResourcesCommand(Guid EmployeeId) : ICommand;
