﻿using EmployeeControl.Application.Common.Constants;
using EmployeeControl.Application.Common.Exceptions;
using EmployeeControl.Application.Common.Extensions;
using EmployeeControl.Application.Common.Interfaces.Common;
using EmployeeControl.Application.Common.Interfaces.Features.Identity;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Application.Localizations;
using EmployeeControl.Domain.Constants;
using EmployeeControl.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace EmployeeControl.Application.Features.Employees.Commands.DeactivateEmployee;

internal class DeactivateEmployeeHandler(
    UserManager<ApplicationUser> userManager,
    IIdentityService identityService,
    IValidationFailureService validationFailureService,
    IStringLocalizer<IdentityLocalizer> localizer)
    : IRequestHandler<DeactivateEmployeeCommand, Result>
{
    public async Task<Result> Handle(DeactivateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var user = userManager.Users.SingleOrDefault(au => au.Id == request.EmployeeId)
                   ?? throw new NotFoundException(nameof(ApplicationUser), nameof(ApplicationUser.Id));

        // La cuenta del Administrator, no se puede desactivar.
        var isAdministrator = await identityService.IsInRoleAsync(user.Id, Roles.Admin);

        if (isAdministrator)
        {
            var message = localizer["No tiene permisos para desactivar esta cuenta"];
            validationFailureService.AddAndRaiseException(ValidationErrorsKeys.NotificationErrors, message);
        }

        user.Active = false;

        var result = await userManager.UpdateAsync(user);

        return result.ToApplicationResult();
    }
}
