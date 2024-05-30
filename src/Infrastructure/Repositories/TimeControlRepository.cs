using EmployeeControl.Application.Common.Constants;
using EmployeeControl.Application.Common.Extensions;
using EmployeeControl.Application.Common.Interfaces.Common;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Localization;
using EmployeeControl.Application.Common.Services.Hubs;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Entities;
using EmployeeControl.Domain.Enums;
using EmployeeControl.Domain.Exceptions;
using EmployeeControl.Domain.Repositories;
using EmployeeControl.Domain.Validators;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace EmployeeControl.Infrastructure.Repositories;

public class TimeControlRepository(
    IDateTimeProvider dateTimeProvider,
    ITimesControlValidator timesControlValidator,
    IApplicationDbContext context,
    ICompanySettingsRepository companySettingsRepository,
    IHubContext<NotificationTimeControlIncidenceHub> hubContext,
    IStringLocalizer<TimeControlResource> localizer)
    : ITimeControlRepository
{
    public int CountIncidences()
    {
        var incidences = GetTimesControlIncidencesQueryable().Count();

        return incidences;
    }

    public async Task<TimeControl> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await context
                         .TimeControls
                         .SingleOrDefaultAsync(tc => tc.Id.Equals(id), cancellationToken)
                     ?? throw new NotFoundException(nameof(TimeControl), nameof(TimeControl.Id));

        return result;
    }

    public async Task<TimeControl?> GetTimeStateOpenByEmployeeIdAsync(Guid employeeId, CancellationToken cancellationToken)
    {
        var timeControl = await context
            .TimeControls
            .SingleOrDefaultAsync(ts => ts.UserId == employeeId && ts.TimeState == TimeState.Open, cancellationToken);

        return timeControl;
    }

    public async Task<IEnumerable<IGrouping<int, TimeControl>>> GetRangeByEmployeeIdAsync(
        Guid employeeId,
        DateTimeOffset from,
        DateTimeOffset to,
        CancellationToken cancellationToken)
    {
        var timeControlGroups = await context
            .TimeControls
            .Where(
                tc => (tc.UserId == employeeId && tc.Start >= from && tc.Start <= to)
                      || (tc.UserId == employeeId && tc.Finish <= to && tc.Finish >= from))
            .GroupBy(tc => tc.Start.Day)
            .ToListAsync(cancellationToken);

        return timeControlGroups;
    }

    public IQueryable<TimeControl> GetWithUserQueryable()
    {
        var timesControl = context
            .TimeControls
            .Include(tc => tc.User);

        return timesControl;
    }

    public async Task<TimeState> GetTimeStateByEmployeeAsync(User user, CancellationToken cancellationToken)
    {
        var timeControl = await context
            .TimeControls
            .SingleOrDefaultAsync(ct => ct.TimeState == TimeState.Open && ct.UserId == user.Id, cancellationToken);

        if (timeControl is null)
        {
            return TimeState.Close;
        }

        // Si el tiempo ha superado las 23:59:59 respecto al día que se inicializó el sistema lo cierra y lo reporta como alerta.
        // El tiempo es en base al timezone de la compañía.
        var dateTimeZone = await companySettingsRepository.ConvertToTimezoneCurrentCompanyAsync(
            dateTimeProvider.EndOfDay(dateTimeProvider.UtcNow),
            cancellationToken);

        if (timeControl.Start.Day != dateTimeZone.Day)
        {
            await FinishAsync(user, DeviceType.System, ClosedBy.System, null, null, cancellationToken);
        }

        return timeControl.TimeState;
    }

    public IQueryable<TimeControl> GetTimesControlIncidencesQueryable()
    {
        var timesControl = context.TimeControls.Where(tc => tc.Incidence);

        return timesControl;
    }

    public async Task<TimeControl> CloseIncidenceByTimeControlAsync(TimeControl timeControl, CancellationToken cancellationToken)
    {
        timeControl.Incidence = false;

        context.TimeControls.Update(timeControl);
        await context.SaveChangesAsync(cancellationToken);

        // Notificar SignalR de una nueva incidencia.
        await hubContext.Clients.All.SendAsync(HubNames.TimeControlIncidences, cancellationToken);

        return timeControl;
    }

    public async Task<TimeControl> CreateWithOutFinishAsync(TimeControl timeControl, CancellationToken cancellationToken)
    {
        var result = Result.Create();
        result = await timesControlValidator.ValidateCreateAsync(timeControl, result, cancellationToken);
        result.RaiseBadRequest();

        await context.TimeControls.AddAsync(timeControl, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return timeControl;
    }

    public async Task<TimeControl> CreateWithFinishAsync(TimeControl timeControl, CancellationToken cancellationToken)
    {
        var result = Result.Create();
        result = await timesControlValidator.ValidateUpdateAsync(timeControl, result, cancellationToken);
        result.RaiseBadRequest();

        await context.TimeControls.AddAsync(timeControl, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return timeControl;
    }

    public async Task<TimeControl> StartAsync(
        User user,
        DeviceType deviceType,
        double? latitude,
        double? longitude,
        CancellationToken cancellationToken)
    {
        var timeControl = new TimeControl
        {
            UserId = user.Id,
            Start = dateTimeProvider.UtcNow,
            Finish = dateTimeProvider.UtcNow,
            DeviceTypeStart = deviceType,
            LatitudeStart = latitude,
            LongitudeStart = longitude
        };

        // Validaciones.
        var result = Result.Create();
        result = await timesControlValidator.ValidateCreateAsync(timeControl, result, cancellationToken);
        result.RaiseBadRequest();

        await context.TimeControls.AddAsync(timeControl, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return timeControl;
    }

    public async Task<TimeControl> UpdateAsync(TimeControl timeControl, CancellationToken cancellationToken)
    {
        var result = Result.Create();
        result = await timesControlValidator.ValidateUpdateAsync(timeControl, result, cancellationToken);
        result.RaiseBadRequest();

        context.TimeControls.Update(timeControl);
        await context.SaveChangesAsync(cancellationToken);

        return timeControl;
    }

    public async Task<TimeControl> FinishAsync(
        User user,
        DeviceType deviceType,
        ClosedBy closedBy,
        double? latitude,
        double? longitude,
        CancellationToken cancellationToken)
    {
        var timeControl = await context
                              .TimeControls
                              .SingleOrDefaultAsync(
                                  tc => tc.TimeState == TimeState.Open && tc.UserId == user.Id,
                                  cancellationToken)
                          ?? throw new NotFoundException(nameof(TimeControl), nameof(TimeControl.UserId));

        if (timeControl.ClosedBy != ClosedBy.Unclosed)
        {
            var message = localizer["No hay un tiempo inicializado."];
            Result.Failure(ValidationErrorsKeys.NotificationErrors, message).RaiseBadRequest();
        }

        timeControl.Finish = dateTimeProvider.UtcNow;
        timeControl.ClosedBy = closedBy;
        timeControl.TimeState = TimeState.Close;
        timeControl.DeviceTypeFinish = deviceType;
        timeControl.LatitudeFinish = latitude;
        timeControl.LongitudeFinish = longitude;

        await UpdateAsync(timeControl, cancellationToken);

        return timeControl;
    }

    public IQueryable<TimeControl> GetWithUserByEmployeeIdQueryable(Guid employeeId)
    {
        var timesControl = context
            .TimeControls
            .Include(tc => tc.User)
            .Where(tc => tc.UserId == employeeId);

        return timesControl;
    }

    public async Task<TimeControl> CloseIncidenceByTimeControlIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var timeControl = await GetByIdAsync(id, cancellationToken);
        await CloseIncidenceByTimeControlAsync(timeControl, cancellationToken);

        return timeControl;
    }

    public async Task<TimeControl> GetWithEmployeeInfoByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await context
                         .TimeControls
                         .Include(tc => tc.User)
                         .SingleOrDefaultAsync(tc => tc.Id.Equals(id), cancellationToken)
                     ?? throw new NotFoundException(nameof(TimeControl), nameof(TimeControl.Id));

        return result;
    }
}
