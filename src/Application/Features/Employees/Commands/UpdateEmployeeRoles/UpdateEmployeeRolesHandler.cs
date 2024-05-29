using EmployeeControl.Application.Common.Extensions;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Repositories;

namespace EmployeeControl.Application.Features.Employees.Commands.UpdateEmployeeRoles;

internal class UpdateEmployeeRolesHandler(IUserRepository userRepository)
    : ICommandHandler<UpdateEmployeeRolesCommand>
{
    public async Task<Result> Handle(UpdateEmployeeRolesCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.EmployeeId);
        var identityResult = await userRepository.UpdateRolesByUserIdAsync(user, request.RolesToAdd, cancellationToken);

        return identityResult.ToResult();
    }
}
