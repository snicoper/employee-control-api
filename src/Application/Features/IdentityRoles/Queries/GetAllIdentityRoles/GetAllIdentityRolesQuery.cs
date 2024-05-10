using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;

namespace EmployeeControl.Application.Features.IdentityRoles.Queries.GetAllIdentityRoles;

[Authorize(Roles = Roles.HumanResources)]
public record GetAllIdentityRolesQuery : IQuery<ICollection<GetAllIdentityRolesResponse>>;
