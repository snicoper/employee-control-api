using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;

namespace EmployeeControl.Application.Features.CompanyTasks.Commands.AssignEmployeesToTask;

[Authorize(Roles = Roles.HumanResources)]
public record AssignEmployeesToTaskCommand(Guid Id, List<Guid> EmployeeIds) : ICommand;
