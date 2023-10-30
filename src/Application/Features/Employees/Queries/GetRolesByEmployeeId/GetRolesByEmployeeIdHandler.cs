using AutoMapper;
using EmployeeControl.Application.Common.Exceptions;
using EmployeeControl.Application.Common.Interfaces.Features.Identity;
using EmployeeControl.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EmployeeControl.Application.Features.Employees.Queries.GetRolesByEmployeeId;

internal class GetRolesByEmployeeIdHandler(
        UserManager<ApplicationUser> userManager,
        IIdentityRoleService identityRoleService,
        IMapper mapper)
    : IRequestHandler<GetRolesByEmployeeIdQuery, ICollection<GetRolesByEmployeeIdResponse>>
{
    public async Task<ICollection<GetRolesByEmployeeIdResponse>> Handle(
        GetRolesByEmployeeIdQuery request,
        CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.EmployeeId)
                   ?? throw new NotFoundException(nameof(ApplicationUser), nameof(ApplicationUser.Id));

        var identityRoles = await identityRoleService.GetRolesByUseAsync(user);

        var result = mapper.Map<List<IdentityRole>, ICollection<GetRolesByEmployeeIdResponse>>(identityRoles);

        return result;
    }
}
