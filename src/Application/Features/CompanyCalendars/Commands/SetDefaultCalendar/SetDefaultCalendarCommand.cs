using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;

namespace EmployeeControl.Application.Features.CompanyCalendars.Commands.SetDefaultCalendar;

[Authorize(Roles = Roles.HumanResources)]
public record SetDefaultCalendarCommand(Guid Id) : ICommand;
