using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;

namespace EmployeeControl.Application.Features.TimesControl.Queries.GetTimesControlByEmployeeIdPaginated;

[Authorize(Roles = Roles.Staff)]
public record GetTimesControlByEmployeeIdPaginatedQuery(
    string EmployeeId,
    DateTimeOffset? From,
    DateTimeOffset? To,
    RequestData RequestData)
    : IQuery<ResponseData<GetTimesControlByEmployeeIdPaginatedResponse>>;
