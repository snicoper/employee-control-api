using MediatR;

namespace EmployeeControl.Application.Features.CompanyTasks.Queries.GetEmployeesUnassignedTaskByCompanyTaskId;

public record GetEmployeesUnassignedTaskByCompanyTaskIdQuery(string Id)
    : IRequest<ICollection<GetEmployeesUnassignedTaskByCompanyTaskIdResponse>>;
