using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;

namespace EmployeeControl.Application.Features.CompanyCalendarHolidays.Queries.
    GetCompanyCalendarHolidaysByCompanyCalendarIdAndYear;

[Authorize(Roles = Roles.Employee)]
public record GetCompanyCalendarHolidaysByCompanyCalendarIdAndYearQuery(Guid CompanyCalendarId, int Year)
    : IQuery<ICollection<GetCompanyCalendarHolidaysByCompanyCalendarIdAndYearResponse>>;
