using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;

namespace EmployeeControl.Application.Features.Departments.Queries.GetDepartmentById;

[Authorize(Roles = Roles.Employee)]
public record GetDepartmentByIdQuery(Guid DepartmentId) : IQuery<GetDepartmentByIdResponse>;
