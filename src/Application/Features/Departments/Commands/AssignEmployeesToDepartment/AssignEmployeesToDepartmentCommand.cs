using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;

namespace EmployeeControl.Application.Features.Departments.Commands.AssignEmployeesToDepartment;

[Authorize(Roles = Roles.HumanResources)]
public record AssignEmployeesToDepartmentCommand(string Id, List<string> EmployeeIds) : ICommand;
