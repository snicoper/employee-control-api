using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;

namespace EmployeeControl.Application.Features.CompanyCalendars.Queries.GetCompanyCalendarById;

[Authorize(Roles = Roles.HumanResources)]
public record GetCompanyCalendarByIdQuery(Guid Id) : IQuery<GetCompanyCalendarByIdResponse>;
