using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;

namespace EmployeeControl.Application.Features.TimesControl.Commands.DeleteTimeControl;

[Authorize(Roles = Roles.HumanResources)]
public record DeleteTimeControlCommand(Guid Id) : ICommand;
