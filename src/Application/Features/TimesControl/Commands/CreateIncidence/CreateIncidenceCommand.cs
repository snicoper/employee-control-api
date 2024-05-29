using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;

namespace EmployeeControl.Application.Features.TimesControl.Commands.CreateIncidence;

[Authorize(Roles = Roles.Employee)]
public record CreateIncidenceCommand(Guid TimeControlId, string IncidenceDescription)
    : ICommand;
