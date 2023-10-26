using MediatR;

namespace EmployeeControl.Application.Features.IdentityRoles.Queries.GetAllIdentityRoles;

public record GetAllIdentityRolesQuery : IRequest<ICollection<GetAllIdentityRolesResponse>>;
