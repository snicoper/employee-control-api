﻿using AutoMapper;
using EmployeeControl.Application.Common.Constants;
using EmployeeControl.Application.Common.Exceptions;
using EmployeeControl.Application.Common.Interfaces.Common;
using EmployeeControl.Application.Common.Interfaces.Entities.Identity;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Application.Localizations;
using EmployeeControl.Domain.Constants;
using EmployeeControl.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace EmployeeControl.Application.Features.Employees.Commands.UpdateEmployee;

internal class UpdateEmployeeHandler(
        IIdentityService identityService,
        UserManager<ApplicationUser> userManager,
        IMapper mapper,
        ICurrentUserService currentUserService,
        IValidationFailureService validationFailureService,
        IStringLocalizer<IdentityLocalizer> localizer)
    : IRequestHandler<UpdateEmployeeCommand, Result>
{
    public async Task<Result> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.Id);
        user = user ?? throw new NotFoundException(nameof(ApplicationUser), nameof(ApplicationUser.Id));

        // Un usuario no puede editarse asi mismo.
        if (currentUserService.Id == user.Id)
        {
            var message = localizer["No te puedes editar a ti mismo."];
            validationFailureService.Add(ValidationErrorsKeys.NonFieldErrors, message);
        }

        // No es posible editar a un usuario con role.
        if (await identityService.IsInRoleAsync(user.Id, Roles.EnterpriseAdministrator))
        {
            var message = localizer["No se puede editar a un administrador."];
            validationFailureService.Add(ValidationErrorsKeys.NonFieldErrors, message);
        }

        validationFailureService.RaiseExceptionIfExistsErrors();

        // Update employee.
        var userUpdate = mapper.Map(request, user);
        var result = await identityService.UpdateAccountAsync(userUpdate, cancellationToken);

        return result;
    }
}