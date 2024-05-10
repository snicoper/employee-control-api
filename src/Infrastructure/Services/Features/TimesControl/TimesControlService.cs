﻿using EmployeeControl.Application.Common.Constants;
using EmployeeControl.Application.Common.Exceptions;
using EmployeeControl.Application.Common.Interfaces.Common;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Features.CompaniesSettings;
using EmployeeControl.Application.Common.Interfaces.Features.TimesControl;
using EmployeeControl.Application.Common.Localization;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Application.Common.Services.Hubs;
using EmployeeControl.Domain.Entities;
using EmployeeControl.Domain.Enums;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace EmployeeControl.Infrastructure.Services.Features.TimesControl;

public class TimesControlService(
    IDateTimeService dateTimeService,
    ITimesControlValidatorService timesControlValidatorService,
    IApplicationDbContext context,
    ICompanySettingsService companySettingsService,
    IHubContext<NotificationTimeControlIncidenceHub> hubContext,
    IStringLocalizer<TimeControlResource> localizer)
    : ITimesControlService
{
    public async Task<TimeControl> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var result = await context
                         .TimeControls
                         .SingleOrDefaultAsync(tc => tc.Id.Equals(id), cancellationToken) ??
                     throw new NotFoundException(nameof(TimeControl), nameof(TimeControl.Id));

        return result;
    }

    public async Task<TimeControl> GetWithEmployeeInfoByIdAsync(string id, CancellationToken cancellationToken)
    {
        var result = await context
                         .TimeControls
                         .Include(tc => tc.User)
                         .SingleOrDefaultAsync(tc => tc.Id.Equals(id), cancellationToken) ??
                     throw new NotFoundException(nameof(TimeControl), nameof(TimeControl.Id));

        return result;
    }

    public async Task<TimeControl?> GetTimeStateOpenByEmployeeIdAsync(string employeeId, CancellationToken cancellationToken)
    {
        var timeControl = await context
            .TimeControls
            .SingleOrDefaultAsync(ts => ts.UserId == employeeId && ts.TimeState == TimeState.Open, cancellationToken);

        return timeControl;
    }

    public async Task<IEnumerable<IGrouping<int, TimeControl>>> GetRangeByEmployeeIdAsync(
        string employeeId,
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

    public IQueryable<TimeControl> GetWithUserByEmployeeIdQueryable(string employeeId)
    {
        var timesControl = context
            .TimeControls
            .Include(tc => tc.User)
            .Where(tc => tc.UserId == employeeId);

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
        var datetimeZone = await companySettingsService.ConvertToTimezoneCurrentCompanyAsync(
            dateTimeService.EndOfDay(dateTimeService.UtcNow),
            cancellationToken);

        if (timeControl.Start.Day != datetimeZone.Day)
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

    public int CountIncidences()
    {
        var incidences = GetTimesControlIncidencesQueryable().Count();

        return incidences;
    }

    public async Task<TimeControl> CloseIncidenceByTimeControlIdAsync(string id, CancellationToken cancellationToken)
    {
        var timeControl = await GetByIdAsync(id, cancellationToken);
        await CloseIncidenceByTimeControlAsync(timeControl, cancellationToken);

        return timeControl;
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
        result = await timesControlValidatorService.ValidateCreateAsync(timeControl, result, cancellationToken);
        result.RaiseBadRequestIfResultFailure();

        await context.TimeControls.AddAsync(timeControl, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return timeControl;
    }

    public async Task<TimeControl> CreateWithFinishAsync(TimeControl timeControl, CancellationToken cancellationToken)
    {
        var result = Result.Create();
        result = await timesControlValidatorService.ValidateUpdateAsync(timeControl, result, cancellationToken);
        result.RaiseBadRequestIfResultFailure();

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
            Start = dateTimeService.UtcNow,
            Finish = dateTimeService.UtcNow,
            DeviceTypeStart = deviceType,
            LatitudeStart = latitude,
            LongitudeStart = longitude
        };

        // Validaciones.
        var result = Result.Create();
        result = await timesControlValidatorService.ValidateCreateAsync(timeControl, result, cancellationToken);
        result.RaiseBadRequestIfResultFailure();

        await context.TimeControls.AddAsync(timeControl, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return timeControl;
    }

    public async Task<TimeControl> UpdateAsync(TimeControl timeControl, CancellationToken cancellationToken)
    {
        var result = Result.Create();
        result = await timesControlValidatorService.ValidateUpdateAsync(timeControl, result, cancellationToken);
        result.RaiseBadRequestIfResultFailure();

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
                                  cancellationToken) ??
                          throw new NotFoundException(nameof(TimeControl), nameof(TimeControl.UserId));

        if (timeControl.ClosedBy != ClosedBy.Unclosed)
        {
            var message = localizer["No hay un tiempo inicializado."];
            Result.Failure(ValidationErrorsKeys.NotificationErrors, message).RaiseBadRequest();
        }

        timeControl.Finish = dateTimeService.UtcNow;
        timeControl.ClosedBy = closedBy;
        timeControl.TimeState = TimeState.Close;
        timeControl.DeviceTypeFinish = deviceType;
        timeControl.LatitudeFinish = latitude;
        timeControl.LongitudeFinish = longitude;

        await UpdateAsync(timeControl, cancellationToken);

        return timeControl;
    }
}
