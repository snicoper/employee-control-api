using EmployeeControl.Application.Common.Constants;
using EmployeeControl.Application.Common.Interfaces.Common;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Localization;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Entities;
using EmployeeControl.Domain.Enums;
using EmployeeControl.Domain.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace EmployeeControl.Infrastructure.Validators;

public class TimesControlValidator(
    IApplicationDbContext context,
    IStringLocalizer<TimeControlResource> localizer,
    IDateTimeProvider dateTimeProvider)
    : ITimesControlValidator
{
    public async Task<Result> ValidateCreateAsync(TimeControl timeControl, Result result, CancellationToken cancellationToken)
    {
        // El Finish no puede ser menor al Start.
        if (timeControl.Finish < timeControl.Start)
        {
            result.AddError(
                ValidationErrorsKeys.NotificationErrors,
                localizer["El tiempo final no puede ser antes que el tiempo de inicio."]);

            return result;
        }

        // Comprueba si existe algún tiempo cerrado en el rango del tiempo a crear.
        var checkTime = await context
            .TimeControls
            .AnyAsync(
                tc => tc.UserId == timeControl.UserId
                    && tc.TimeState == TimeState.Close
                    && timeControl.Start >= tc.Start
                    && timeControl.Start <= tc.Finish,
                cancellationToken);

        if (checkTime)
        {
            result.AddError(
                ValidationErrorsKeys.NotificationErrors,
                localizer["El tiempo coincide con algún tiempo creado y no es posible iniciar el tiempo."]);

            return result;
        }

        // Comprueba si hay algún tiempo abierto que coincide con el rango de tiempo a crear.
        checkTime = await context
            .TimeControls
            .AnyAsync(
                tc => tc.UserId == timeControl.UserId && tc.TimeState == TimeState.Open
                    && timeControl.Start >= tc.Start && timeControl.Start <= dateTimeProvider.UtcNow,
                cancellationToken);

        if (!checkTime)
        {
            return result;
        }

        result.AddError(
            ValidationErrorsKeys.NotificationErrors,
            localizer["El tiempo coincide con algún tiempo abierto y no es posible iniciar el tiempo."]);

        return result;
    }

    public async Task<Result> ValidateUpdateAsync(TimeControl timeControl, Result result, CancellationToken cancellationToken)
    {
        // Los tiempos iniciados, no se pueden editar.
        if (timeControl.TimeState == TimeState.Open)
        {
            result.AddError(
                ValidationErrorsKeys.NotificationErrors,
                localizer["El tiempo esta actualmente iniciado."]);

            return result;
        }

        // El Finish no puede ser menor al Start.
        if (timeControl.Finish < timeControl.Start)
        {
            result.AddError(
                ValidationErrorsKeys.NotificationErrors,
                localizer["El tiempo final no puede ser antes que el tiempo de inicio."]);

            return result;
        }

        // Comprueba si existe algún tiempo en el rango del tiempo a actualizar.
        var checkTime = await context
            .TimeControls
            .AnyAsync(
                tc =>
                    tc.Id != timeControl.Id &&
                    tc.UserId == timeControl.UserId &&
                    tc.TimeState == TimeState.Close &&
                    ((timeControl.Start >= tc.Start && timeControl.Start <= tc.Finish)
                        || (timeControl.Finish >= tc.Start && timeControl.Finish <= tc.Finish)),
                cancellationToken);

        if (checkTime)
        {
            result.AddError(
                ValidationErrorsKeys.NotificationErrors,
                localizer["El tiempo coincide con algún tiempo creado y no es posible actualizar el tiempo."]);

            return result;
        }

        // Comprueba si hay algún tiempo abierto que coincide con el rango de tiempo a actualizar.
        checkTime = await context
            .TimeControls
            .AnyAsync(
                tc => tc.UserId == timeControl.UserId && tc.Id != timeControl.Id && tc.TimeState == TimeState.Open
                    && ((timeControl.Start >= tc.Start && timeControl.Start <= dateTimeProvider.UtcNow)
                        || (timeControl.Finish >= tc.Start && timeControl.Finish <= dateTimeProvider.UtcNow)),
                cancellationToken);

        if (!checkTime)
        {
            return result;
        }

        result.AddError(
            ValidationErrorsKeys.NotificationErrors,
            localizer["El tiempo coincide con algún tiempo abierto y no es posible actualizar el tiempo."]);

        return result;
    }
}
