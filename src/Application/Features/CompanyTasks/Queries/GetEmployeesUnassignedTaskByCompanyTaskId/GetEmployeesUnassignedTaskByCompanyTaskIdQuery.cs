using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;

namespace EmployeeControl.Application.Features.CompanyTasks.Queries.GetEmployeesUnassignedTaskByCompanyTaskId;

[Authorize(Roles = Roles.HumanResources)]
public record GetEmployeesUnassignedTaskByCompanyTaskIdQuery(string Id)
    : IQuery<List<GetEmployeesUnassignedTaskByCompanyTaskIdResponse>>;
