using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;

namespace EmployeeControl.Application.Features.CompanyTasks.Queries.GetCompanyTasksByEmployeeIdPaginated;

[Authorize(Roles = Roles.HumanResources)]
public record GetCompanyTasksByEmployeeIdPaginatedQuery(RequestData RequestData, Guid EmployeeId)
    : IQuery<ResponseData<GetCompanyTasksByEmployeeIdPaginatedResponse>>;
