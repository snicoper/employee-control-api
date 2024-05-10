using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;

namespace EmployeeControl.Application.Features.CompanyTasks.Queries.GetCompanyTasksById;

[Authorize(Roles = Roles.HumanResources)]
public record GetCompanyTasksByIdQuery(string Id) : IQuery<GetCompanyTasksByIdResponse>;
