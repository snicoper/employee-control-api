using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;

namespace EmployeeControl.Application.Features.CompanyTasks.Commands.ActivateCompanyTask;

[Authorize(Roles = Roles.HumanResources)]
public record ActivateCompanyTaskCommand(Guid CompanyTaskId)
    : ICommand;
