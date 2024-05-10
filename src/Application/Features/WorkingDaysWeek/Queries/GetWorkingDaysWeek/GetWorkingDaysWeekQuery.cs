using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;

namespace EmployeeControl.Application.Features.WorkingDaysWeek.Queries.GetWorkingDaysWeek;

[Authorize(Roles = Roles.Employee)]
public record GetWorkingDaysWeekQuery : IQuery<GetWorkingDaysWeekResponse>;
