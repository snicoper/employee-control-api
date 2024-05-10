using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;

namespace EmployeeControl.Application.Features.CompanyTasks.Queries.GetEmployeesByCompanyTaskIdPaginated;

[Authorize(Roles = Roles.HumanResources)]
public record GetEmployeesByCompanyTaskIdPaginatedQuery(RequestData RequestData, string CompanyTaskId)
    : IQuery<ResponseData<GetEmployeesByCompanyTaskIdPaginatedResponse>>;
