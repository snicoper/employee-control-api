using MediatR;

namespace EmployeeControl.Application.Features.Employees.Queries.GetRolesByEmployeeId;

public record GetRolesByEmployeeIdQuery(string EmployeeId) : IRequest<ICollection<GetRolesByEmployeeIdResponse>>;
