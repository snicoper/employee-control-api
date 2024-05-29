using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;

namespace EmployeeControl.Application.Features.TimesControl.Commands.CloseIncidence;

[Authorize(Roles = Roles.HumanResources)]
public record CloseIncidenceCommand(Guid Id)
    : ICommand;
