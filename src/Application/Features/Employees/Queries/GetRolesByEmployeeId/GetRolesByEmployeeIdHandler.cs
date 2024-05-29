using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace EmployeeControl.Application.Features.Employees.Queries.GetRolesByEmployeeId;

internal class GetRolesByEmployeeIdHandler(
    IUserRepository userRepository,
    IUserRoleRepository userRoleRepository,
    IMapper mapper)
    : IQueryHandler<GetRolesByEmployeeIdQuery, ICollection<GetRolesByEmployeeIdResponse>>
{
    public async Task<Result<ICollection<GetRolesByEmployeeIdResponse>>> Handle(
        GetRolesByEmployeeIdQuery request,
        CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.EmployeeId);
        var identityRoles = await userRoleRepository.GetRolesByUseAsync(user);
        var result = mapper.Map<List<IdentityRole>, ICollection<GetRolesByEmployeeIdResponse>>(identityRoles);

        return Result.Success(result);
    }
}
