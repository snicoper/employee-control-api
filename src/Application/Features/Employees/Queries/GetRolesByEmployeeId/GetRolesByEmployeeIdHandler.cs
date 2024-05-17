using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Features.Identity;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Entities;
using EmployeeControl.Domain.Repositories;

namespace EmployeeControl.Application.Features.Employees.Queries.GetRolesByEmployeeId;

internal class GetRolesByEmployeeIdHandler(
    IUserRepository userRepository,
    IIdentityRoleService identityRoleService,
    IMapper mapper)
    : IQueryHandler<GetRolesByEmployeeIdQuery, ICollection<GetRolesByEmployeeIdResponse>>
{
    public async Task<Result<ICollection<GetRolesByEmployeeIdResponse>>> Handle(
        GetRolesByEmployeeIdQuery request,
        CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.EmployeeId);
        var identityRoles = await identityRoleService.GetRolesByUseAsync(user);
        var result = mapper.Map<List<ApplicationRole>, ICollection<GetRolesByEmployeeIdResponse>>(identityRoles);

        return Result.Success(result);
    }
}
