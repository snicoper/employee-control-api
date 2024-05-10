using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;

namespace EmployeeControl.Application.Features.CompanyCalendars.Queries.GetCompanyCalendars;

[Authorize(Roles = Roles.Employee)]
public record GetCompanyCalendarsQuery : IQuery<ICollection<GetCompanyCalendarsResponse>>;
