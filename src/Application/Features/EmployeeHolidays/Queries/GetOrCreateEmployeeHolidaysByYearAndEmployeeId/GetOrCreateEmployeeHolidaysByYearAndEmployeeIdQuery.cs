using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;

namespace EmployeeControl.Application.Features.EmployeeHolidays.Queries.GetOrCreateEmployeeHolidaysByYearAndEmployeeId;

[Authorize(Roles = Roles.Employee)]
public record GetOrCreateEmployeeHolidaysByYearAndEmployeeIdQuery(int Year, Guid EmployeeId)
    : IQuery<GetOrCreateEmployeeHolidaysByYearAndEmployeeIdResponse>;
