using EmployeeControl.Application.Common.Constants;
using EmployeeControl.Application.Common.Extensions;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Localization;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Constants;
using EmployeeControl.Domain.Entities;
using EmployeeControl.Domain.Exceptions;
using EmployeeControl.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace EmployeeControl.Application.Features.Employees.Commands.DeactivateEmployee;

internal class DeactivateEmployeeHandler(
    UserManager<User> userManager,
    IUserRepository userRepository,
    IStringLocalizer<IdentityResource> localizer)
    : ICommandHandler<DeactivateEmployeeCommand>
{
    public async Task<Result> Handle(DeactivateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var user = userManager.Users.SingleOrDefault(au => au.Id == request.EmployeeId)
                   ?? throw new NotFoundException(nameof(User), nameof(User.Id));

        // La cuenta del Administrator no se puede modificar el estado.
        var isAdministrator = await userRepository.IsInRoleAsync(user.Id, Roles.Admin);

        if (isAdministrator)
        {
            var message = localizer["No tiene permisos para desactivar esta cuenta"];
            Result.Failure(ValidationErrorsKeys.NotificationErrors, message).RaiseBadRequest();
        }

        user.Active = false;
        var result = await userManager.UpdateAsync(user);

        return result.ToApplicationResult();
    }
}
