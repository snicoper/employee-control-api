using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;

namespace EmployeeControl.Application.Features.TimesControl.Queries.GetTimeControlRangeByEmployeeId;

[Authorize(Roles = Roles.Employee)]
public record GetTimeControlRangeByEmployeeIdQuery(string EmployeeId, DateTimeOffset From, DateTimeOffset To)
    : IQuery<List<GetTimeControlRangeByEmployeeIdResponse>>;
