using EmployeeControl.Application.Common.Constants;
using EmployeeControl.Application.Common.Extensions;
using EmployeeControl.Application.Common.Interfaces.Features.Identity;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Localization;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Constants;
using EmployeeControl.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace EmployeeControl.Application.Features.Employees.Commands.ActivateEmployee;

internal class ActivateEmployeeHandler(
    UserManager<User> userManager,
    IIdentityService identityService,
    IStringLocalizer<IdentityResource> localizer)
    : ICommandHandler<ActivateEmployeeCommand>
{
    public async Task<Result> Handle(ActivateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var user = await identityService.GetByIdAsync(request.EmployeeId);

        // La cuenta del Administrator no se puede modificar el estado.
        var isAdministrator = await identityService.IsInRoleAsync(user.Id, Roles.Admin);

        if (isAdministrator)
        {
            var message = localizer["No tiene permisos para activar esta cuenta"];
            Result.Failure(ValidationErrorsKeys.NotificationErrors, message).RaiseBadRequest();
        }

        user.Active = true;
        var result = await userManager.UpdateAsync(user);

        return result.ToApplicationResult();
    }
}
