using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;

namespace EmployeeControl.Application.Features.TimesControl.Commands.UpdateTimeControl;

[Authorize(Roles = Roles.HumanResources)]
public record UpdateTimeControlCommand(string Id, DateTimeOffset Start, DateTimeOffset Finish, bool CloseIncidence)
    : ICommand;
