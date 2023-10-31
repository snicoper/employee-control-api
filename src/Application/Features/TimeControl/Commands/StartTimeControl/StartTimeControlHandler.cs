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

namespace EmployeeControl.Application.Features.TimeControl.Commands.StartTimeControl;

internal class StartTimeControlHandler(
        UserManager<ApplicationUser> userManager,
        IApplicationDbContext context,
        TimeProvider timeProvider,
        IValidationFailureService validationFailureService,
        IEntityValidationService entityValidationService,
        IStringLocalizer<TimeControlLocalizer> localizer)
    : IRequestHandler<StartTimeControlCommand, Result>
{
    public async Task<Result> Handle(StartTimeControlCommand request, CancellationToken cancellationToken)
    {
        var employee = await userManager.FindByIdAsync(request.EmployeeId) ??
                       throw new NotFoundException(nameof(ApplicationUser), nameof(ApplicationUser.Id));

        var timeControlInitialized = await context
            .TimeControls
            .AsNoTracking()
            .SingleOrDefaultAsync(tc => tc.Finish == null, cancellationToken);

        if (timeControlInitialized is not null)
        {
            var message = localizer["Ya hay un tiempo inicializado y no es posible comenzar otro."];
            validationFailureService.AddAndRaiseException(ValidationErrorsKeys.NotificationErrors, message);
        }

        var timeControl = new Domain.Entities.TimeControl
        {
            UserId = request.EmployeeId, Start = timeProvider.GetUtcNow(), CompanyId = employee.CompanyId
        };

        await entityValidationService.CheckEntityCompanyIsOwner(timeControl);
        await context.TimeControls.AddAsync(timeControl, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
