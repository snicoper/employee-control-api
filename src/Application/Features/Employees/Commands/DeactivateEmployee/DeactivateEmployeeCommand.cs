using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;

namespace EmployeeControl.Application.Features.Employees.Commands.DeactivateEmployee;

[Authorize(Roles = Roles.HumanResources)]
public record DeactivateEmployeeCommand(Guid EmployeeId) : ICommand;
