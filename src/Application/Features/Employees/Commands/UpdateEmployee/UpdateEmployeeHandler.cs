using AutoMapper;
using EmployeeControl.Application.Common.Constants;
using EmployeeControl.Application.Common.Interfaces.Common;
using EmployeeControl.Application.Common.Interfaces.Features.Identity;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Interfaces.Validation;
using EmployeeControl.Application.Common.Localization;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Constants;
using Microsoft.Extensions.Localization;

namespace EmployeeControl.Application.Features.Employees.Commands.UpdateEmployee;

internal class UpdateEmployeeHandler(
    IIdentityService identityService,
    IMapper mapper,
    ICurrentUserService currentUserService,
    IValidationResultService validationResultService,
    IStringLocalizer<IdentityResource> localizer)
    : ICommandHandler<UpdateEmployeeCommand>
{
    public async Task<Result> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var result = Result.Create();
        var user = await identityService.GetByIdAsync(request.Id);

        // Un usuario no puede editarse asi mismo.
        if (currentUserService.Id == user.Id)
        {
            var messageError = localizer["No te puedes editar a ti mismo."];
            result.AddError(ValidationErrorsKeys.NonFieldErrors, messageError);
        }

        // No es posible editar a un usuario con role.
        if (await identityService.IsInRoleAsync(user.Id, Roles.Admin))
        {
            var messageError = localizer["No se puede editar a un administrador."];
            result.AddError(ValidationErrorsKeys.NonFieldErrors, messageError);
        }

        result.RaiseBadRequestIfResultFailure();

        validationResultService.RaiseExceptionIfExistsErrors();

        // Update employee.
        var userUpdate = mapper.Map(request, user);
        var identityResult = await identityService.UpdateAsync(userUpdate, cancellationToken);

        return identityResult;
    }
}
