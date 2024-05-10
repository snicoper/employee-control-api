using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;

namespace EmployeeControl.Application.Features.CompanyTasks.Queries.GetCompanyTasksPaginated;

[Authorize(Roles = Roles.HumanResources)]
public record GetCompanyTasksPaginatedQuery(RequestData RequestData)
    : IQuery<ResponseData<GetCompanyTasksPaginatedResponse>>;
