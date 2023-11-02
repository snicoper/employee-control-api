using EmployeeControl.Application.Common.Constants;
using EmployeeControl.Application.Common.Exceptions;
using EmployeeControl.Application.Common.Extensions;
using EmployeeControl.Application.Common.Interfaces.Common;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Features;
using EmployeeControl.Application.Common.Interfaces.Features.TimesControl;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Application.Localizations;
using EmployeeControl.Domain.Entities;
using EmployeeControl.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace EmployeeControl.Infrastructure.Services.Features.TimesControl;

public class TimesControlService(
        TimeProvider timeProvider,
        IEntityValidationService entityValidationService,
        IValidationFailureService validationFailureService,
        IApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        IStringLocalizer<TimeControlLocalizer> localizer)
    : ITimesControlService
{
    public async Task<IEnumerable<IGrouping<int, TimeControl>>> GetRangeByEmployeeIdAsync(
        string employeeId,
        DateTimeOffset from,
        DateTimeOffset to,
        CancellationToken cancellationToken)
    {
        var timeControlGroups = await context
            .TimeControls
            .AsNoTracking()
            .Where(tc => tc.UserId == employeeId && tc.Start >= from && tc.Finish <= to)
            .GroupBy(tc => tc.Start.Day)
            .ToListAsync(cancellationToken);

        // Seleccionar el primer item para comprobar permisos de lectura.
        var firstTimeControl = timeControlGroups.FirstOrDefault()?.FirstOrDefault();

        if (firstTimeControl is null)
        {
            return new List<IGrouping<int, TimeControl>>();
        }

        await entityValidationService.CheckEntityCompanyIsOwner(firstTimeControl);

        return timeControlGroups;
    }

    public async Task<TimeState> GetTimeStateByEmployeeIAsync(string employeeId, CancellationToken cancellationToken)
    {
        var timeControl = await context
            .TimeControls
            .AsNoTracking()
            .SingleOrDefaultAsync(ct => ct.TimeState == TimeState.Open && ct.UserId == employeeId, cancellationToken);

        if (timeControl is null)
        {
            return TimeState.Close;
        }

        // Si el tiempo ha superado las 23:59:59 respecto al día que se inicializó,
        // el sistema lo cierra y lo reporta como alerta.
        if (timeControl.Start.Day != timeProvider.GetUtcNow().Day)
        {
            await FinishAsync(employeeId, ClosedBy.System, cancellationToken);
        }

        await entityValidationService.CheckEntityCompanyIsOwner(timeControl);

        return timeControl.TimeState;
    }

    public async Task<(Result Result, TimeControl TimeControl)> StartAsync(string employeeId, CancellationToken cancellationToken)
    {
        var employee = await userManager.FindByIdAsync(employeeId) ??
                       throw new NotFoundException(nameof(ApplicationUser), nameof(ApplicationUser.Id));

        var timeControlInitialized = await context
            .TimeControls
            .AsNoTracking()
            .SingleOrDefaultAsync(tc => tc.TimeState == TimeState.Open && tc.UserId == employeeId, cancellationToken);

        if (timeControlInitialized is not null)
        {
            var message = localizer["Ya hay un tiempo inicializado y no es posible comenzar otro."];
            validationFailureService.AddAndRaiseException(ValidationErrorsKeys.NotificationErrors, message);
        }

        var now = timeProvider.GetUtcNow();
        var timeControl = new TimeControl { UserId = employeeId, Start = now, Finish = now, CompanyId = employee.CompanyId };

        await entityValidationService.CheckEntityCompanyIsOwner(timeControl);
        await context.TimeControls.AddAsync(timeControl, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return (Result.Success(), timeControl);
    }

    public async Task<(Result Result, TimeControl? TimeControl)> FinishAsync(
        string employeeId,
        ClosedBy closedBy,
        CancellationToken cancellationToken)
    {
        var timeControl = await context
            .TimeControls
            .AsNoTracking()
            .SingleOrDefaultAsync(tc => tc.TimeState == TimeState.Open && tc.UserId == employeeId, cancellationToken);

        if (timeControl?.ClosedBy is null)
        {
            var message = localizer["No hay un tiempo inicializado."];
            validationFailureService.AddAndRaiseException(ValidationErrorsKeys.NotificationErrors, message);

            return (Result.Failure(message), timeControl);
        }

        // Si es cerrado por el sistema, de momento solo hay un motivo que es, tiempo superado.
        timeControl.Finish = closedBy == ClosedBy.System ? timeControl.Finish.EndOfDay(timeProvider) : timeProvider.GetUtcNow();
        timeControl.ClosedBy = closedBy;
        timeControl.TimeState = TimeState.Close;

        await entityValidationService.CheckEntityCompanyIsOwner(timeControl);
        context.TimeControls.Update(timeControl);
        await context.SaveChangesAsync(cancellationToken);

        return (Result.Success(), timeControl);
    }
}
