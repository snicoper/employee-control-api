using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;

namespace EmployeeControl.Application.Features.Departments.Queries.GetDepartmentsByEmployeeIdPaginated;

[Authorize(Roles = Roles.HumanResources)]
public record GetDepartmentsByEmployeeIdPaginatedQuery(string EmployeeId, RequestData RequestData)
    : IQuery<ResponseData<GetDepartmentsByEmployeeIdPaginatedResponse>>;
