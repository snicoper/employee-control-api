using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl.Application.Features.IdentityRoles.Queries.GetAllIdentityRoles;

internal class GetAllIdentityRolesHandler(RoleManager<IdentityRole> roleManager, IMapper mapper)
    : IRequestHandler<GetAllIdentityRolesQuery, ICollection<GetAllIdentityRolesResponse>>
{
    public Task<ICollection<GetAllIdentityRolesResponse>> Handle(GetAllIdentityRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = roleManager
            .Roles
            .AsNoTracking()
            .ToList();

        var result = mapper.Map<List<IdentityRole>, ICollection<GetAllIdentityRolesResponse>>(roles);

        return Task.FromResult(result);
    }
}
