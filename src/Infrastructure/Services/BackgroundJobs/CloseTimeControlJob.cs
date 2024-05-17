﻿using EmployeeControl.Application.Common.Constants;
using EmployeeControl.Application.Common.Interfaces.BackgroundJobs;
using EmployeeControl.Application.Common.Interfaces.Common;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Localization;
using EmployeeControl.Application.Common.Services.Hubs;
using EmployeeControl.Domain.Enums;
using EmployeeControl.Domain.Repositories;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace EmployeeControl.Infrastructure.Services.BackgroundJobs;

public class CloseTimeControlJob(
    ICompanySettingsRepository companySettingsRepository,
    IApplicationDbContext context,
    IStringLocalizer<TimeControlResource> localizer,
    IDateTimeService dateTimeService,
    IHubContext<NotificationTimeControlIncidenceHub> hubContext,
    ILogger<CloseTimeControlJob> logger)
    : ICloseTimeControlJob
{
    public async Task Process()
    {
        logger.LogInformation("Procesando {ControlJob}.", nameof(CloseTimeControlJob));
        var companySettings = await companySettingsRepository.GetCompanySettingsAsync(CancellationToken.None);
        var periodTimeControlMax = companySettings.PeriodTimeControlMax;

        var timesControl = context
            .TimeControls
            .Where(tc => tc.TimeState == TimeState.Open && (dateTimeService.UtcNow - tc.Start).Hours >= periodTimeControlMax)
            .ToList();

        foreach (var timeControl in timesControl)
        {
            timeControl.Finish = timeControl.Start.AddHours(periodTimeControlMax);
            timeControl.ClosedBy = ClosedBy.System;
            timeControl.TimeState = TimeState.Close;
            timeControl.DeviceTypeFinish = DeviceType.System;
            timeControl.Incidence = true;
            timeControl.IncidenceDescription = localizer["Tiempo cerrado por el sistema por exceder el tiempo abierto."];
        }

        context.TimeControls.UpdateRange(timesControl);
        await context.SaveChangesAsync(CancellationToken.None);

        // Notificar SignalR de cierre de una incidencia.
        await hubContext.Clients.All.SendAsync(HubNames.TimeControlIncidences, CancellationToken.None);

        logger.LogInformation("Procesado {ControlJob} con éxito.", nameof(CloseTimeControlJob));
    }
}
