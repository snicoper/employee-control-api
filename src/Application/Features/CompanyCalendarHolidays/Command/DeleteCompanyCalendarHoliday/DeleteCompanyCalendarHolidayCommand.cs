using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;

namespace EmployeeControl.Application.Features.CompanyCalendarHolidays.Command.DeleteCompanyCalendarHoliday;

[Authorize(Roles = Roles.HumanResources)]
public record DeleteCompanyCalendarHolidayCommand(Guid Id)
    : ICommand;
