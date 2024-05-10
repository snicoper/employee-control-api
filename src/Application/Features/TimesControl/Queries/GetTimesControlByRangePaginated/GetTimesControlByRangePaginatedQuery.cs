using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;

namespace EmployeeControl.Application.Features.TimesControl.Queries.GetTimesControlByRangePaginated;

[Authorize(Roles = Roles.Staff)]
public record GetTimesControlByRangePaginatedQuery(DateTimeOffset? From, DateTimeOffset? To, RequestData RequestData)
    : IQuery<ResponseData<GetTimesControlByRangePaginatedResponse>>;
