using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;

namespace EmployeeControl.Application.Features.Employees.Commands.AddRoleHumanResources;

[Authorize(Roles = Roles.Admin)]
public record AddRoleHumanResourcesCommand(string EmployeeId) : ICommand;
