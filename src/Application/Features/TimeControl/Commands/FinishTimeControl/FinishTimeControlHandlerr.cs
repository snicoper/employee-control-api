using EmployeeControl.Application.Common.Constants;
using EmployeeControl.Application.Common.Exceptions;
using EmployeeControl.Application.Common.Interfaces.Common;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Features;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Application.Localizations;
using EmployeeControl.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace EmployeeControl.Application.Features.TimeControl.Commands.FinishTimeControl;

internal class FinishTimeControlHandlerr(
        UserManager<ApplicationUser> userManager,
        IApplicationDbContext context,
        TimeProvider timeProvider,
        IValidationFailureService validationFailureService,
        IEntityValidationService entityValidationService,
        IStringLocalizer<TimeControlLocalizer> localizer)
    : IRequestHandler<FinishTimeControlCommand, Result>
{
    public async Task<Result> Handle(FinishTimeControlCommand request, CancellationToken cancellationToken)
    {
        var employee = await userManager.FindByIdAsync(request.EmployeeId) ??
                       throw new NotFoundException(nameof(ApplicationUser), nameof(ApplicationUser.Id));

        var timeControl = await context
            .TimeControls
            .AsNoTracking()
            .SingleOrDefaultAsync(tc => tc.Finish == null, cancellationToken);

        if (timeControl is null)
        {
            var message = localizer["No hay un tiempo inicializado."];
            validationFailureService.AddAndRaiseException(ValidationErrorsKeys.NotificationErrors, message);

            return Result.Failure();
        }

        timeControl.Finish = timeProvider.GetUtcNow();

        await entityValidationService.CheckEntityCompanyIsOwner(timeControl);
        context.TimeControls.Update(timeControl);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
