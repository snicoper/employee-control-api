using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Features.Identity;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Constants;
using MediatR;

namespace EmployeeControl.Application.Features.Employees.Commands.UpdateEmployeeRoles;

internal class UpdateEmployeeRolesHandler(IIdentityService identityService, IEntityValidationService entityValidationService)
    : IRequestHandler<UpdateEmployeeRolesCommand, Result>
{
    public async Task<Result> Handle(UpdateEmployeeRolesCommand request, CancellationToken cancellationToken)
    {
        // Si el rol de Employee no existe, lo añade.
        if (!request.Roles.Any(r => r.Equals(Roles.Employee)))
        {
            request.Roles.Add(Roles.Employee);
        }

        var user = await identityService.GetByIdAsync(request.EmployeeId);

        await entityValidationService.CheckEntityCompanyIsOwnerAsync(user);

        var resultResponse = await identityService.UpdateRolesByUserIdAsync(user, request.Roles, cancellationToken);

        return resultResponse;
    }
}
