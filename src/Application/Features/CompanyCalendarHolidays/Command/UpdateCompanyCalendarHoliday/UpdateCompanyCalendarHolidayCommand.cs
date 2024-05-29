using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;

namespace EmployeeControl.Application.Features.CompanyCalendarHolidays.Command.UpdateCompanyCalendarHoliday;

[Authorize(Roles = Roles.HumanResources)]
public record UpdateCompanyCalendarHolidayCommand(Guid Id, string Description)
    : ICommand;
