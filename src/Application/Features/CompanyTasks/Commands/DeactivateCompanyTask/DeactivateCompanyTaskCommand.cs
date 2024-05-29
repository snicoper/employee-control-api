using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;

namespace EmployeeControl.Application.Features.CompanyTasks.Commands.DeactivateCompanyTask;

[Authorize(Roles = Roles.HumanResources)]
public record DeactivateCompanyTaskCommand(Guid CompanyTaskId)
    : ICommand;
