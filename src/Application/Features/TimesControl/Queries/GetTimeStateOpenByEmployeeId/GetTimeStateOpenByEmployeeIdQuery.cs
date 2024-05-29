using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;

namespace EmployeeControl.Application.Features.TimesControl.Queries.GetTimeStateOpenByEmployeeId;

[Authorize(Roles = Roles.Employee)]
public record GetTimeStateOpenByEmployeeIdQuery(Guid EmployeeId) : IQuery<GetTimeStateOpenByEmployeeIdResponse>;
