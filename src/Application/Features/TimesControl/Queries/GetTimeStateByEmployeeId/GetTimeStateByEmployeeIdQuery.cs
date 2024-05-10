using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;

namespace EmployeeControl.Application.Features.TimesControl.Queries.GetTimeStateByEmployeeId;

[Authorize(Roles = Roles.Employee)]
public record GetTimeStateByEmployeeIdQuery(string EmployeeId) : IQuery<GetTimeStateByEmployeeIdResponse>;
