using EmployeeControl.Application.Common.Models;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;
using MediatR;

namespace EmployeeControl.Application.Features.CompanyTasks.Queries.GetUsersByCompanyTaskIdPaginated;

[Authorize(Roles = Roles.HumanResources)]
public record GetUsersByCompanyTaskIdPaginatedQuery(RequestData RequestData, string CompanyTaskId)
    : IRequest<ResponseData<GetUsersByCompanyTaskIdPaginatedResponse>>;
