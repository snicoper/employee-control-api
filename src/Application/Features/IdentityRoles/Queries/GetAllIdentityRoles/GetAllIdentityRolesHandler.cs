using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace EmployeeControl.Application.Features.IdentityRoles.Queries.GetAllIdentityRoles;

internal class GetAllIdentityRolesHandler(RoleManager<UserRole> roleManager, IMapper mapper)
    : IQueryHandler<GetAllIdentityRolesQuery, ICollection<GetAllIdentityRolesResponse>>
{
    public Task<Result<ICollection<GetAllIdentityRolesResponse>>> Handle(
        GetAllIdentityRolesQuery request,
        CancellationToken cancellationToken)
    {
        var roles = roleManager
            .Roles
            .ToList();

        var result = mapper.Map<List<UserRole>, ICollection<GetAllIdentityRolesResponse>>(roles);

        return Task.FromResult(Result.Success(result));
    }
}
