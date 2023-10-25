using EmployeeControl.Application.Common.Exceptions;
using EmployeeControl.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EmployeeControl.Application.Features.Employees.Queries.GetRolesByEmployeeId;

internal class GetRolesByEmployeeIdHandler(UserManager<ApplicationUser> userManager)
    : IRequestHandler<GetRolesByEmployeeIdQuery, GetRolesByEmployeeIdResponse>
{
    public async Task<GetRolesByEmployeeIdResponse> Handle(GetRolesByEmployeeIdQuery request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.EmployeeId)
                   ?? throw new NotFoundException(nameof(ApplicationUser), nameof(ApplicationUser.Id));

        var userRoles = await userManager.GetRolesAsync(user) ??
                        throw new NotFoundException(nameof(ApplicationUser), nameof(ApplicationUser.Id));

        throw new NotImplementedException();
    }
}
