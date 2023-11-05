using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Features.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EmployeeControl.Application.Features.Employees.Queries.GetRolesByEmployeeId;

internal class GetRolesByEmployeeIdHandler(
    IIdentityService identityService,
    IIdentityRoleService identityRoleService,
    IMapper mapper)
    : IRequestHandler<GetRolesByEmployeeIdQuery, ICollection<GetRolesByEmployeeIdResponse>>
{
    public async Task<ICollection<GetRolesByEmployeeIdResponse>> Handle(
        GetRolesByEmployeeIdQuery request,
        CancellationToken cancellationToken)
    {
        var user = await identityService.GetByIdAsync(request.EmployeeId);
        var identityRoles = await identityRoleService.GetRolesByUseAsync(user);
        var result = mapper.Map<List<IdentityRole>, ICollection<GetRolesByEmployeeIdResponse>>(identityRoles);

        return result;
    }
}
