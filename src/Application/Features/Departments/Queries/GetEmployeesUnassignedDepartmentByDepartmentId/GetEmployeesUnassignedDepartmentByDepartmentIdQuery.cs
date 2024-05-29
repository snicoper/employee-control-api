using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;

namespace EmployeeControl.Application.Features.Departments.Queries.GetEmployeesUnassignedDepartmentByDepartmentId;

[Authorize(Roles = Roles.HumanResources)]
public record GetEmployeesUnassignedDepartmentByDepartmentIdQuery(Guid Id)
    : IQuery<List<GetEmployeesUnassignedDepartmentByDepartmentIdResponse>>;
