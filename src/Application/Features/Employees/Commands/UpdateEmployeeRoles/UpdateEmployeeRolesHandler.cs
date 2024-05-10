using EmployeeControl.Application.Common.Interfaces.Features.Identity;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;

namespace EmployeeControl.Application.Features.Employees.Commands.UpdateEmployeeRoles;

internal class UpdateEmployeeRolesHandler(IIdentityService identityService)
    : ICommandHandler<UpdateEmployeeRolesCommand>
{
    public async Task<Result> Handle(UpdateEmployeeRolesCommand request, CancellationToken cancellationToken)
    {
        var user = await identityService.GetByIdAsync(request.EmployeeId);
        var resultResponse = await identityService.UpdateRolesByUserIdAsync(user, request.RolesToAdd, cancellationToken);

        return resultResponse;
    }
}
