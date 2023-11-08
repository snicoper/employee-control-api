using EmployeeControl.Application.Common.Constants;
using EmployeeControl.Application.Common.Interfaces.Common;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Features.TimesControl;
using EmployeeControl.Domain.Entities;
using EmployeeControl.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace EmployeeControl.Infrastructure.Services.Features.TimesControl;

public class TimesControlValidatorService(
    IApplicationDbContext context,
    IValidationFailureService validationFailureService,
    IStringLocalizer<TimesControlService> localizer,
    IDateTimeService dateTimeService)
    : ITimesControlValidatorService
{
    public async Task ValidateForCreateAsync(TimeControl timeControl, CancellationToken cancellationToken)
    {
        // Comprueba si existe algún tiempo en el rango del tiempo a crear.
        var checkTime = await context
            .TimeControls
            .AnyAsync(
                tc => tc.UserId == timeControl.UserId && timeControl.Start >= tc.Start && timeControl.Start <= tc.Finish,
                cancellationToken);

        if (checkTime)
        {
            validationFailureService.Add(
                ValidationErrorsKeys.NotificationErrors,
                localizer["El tiempo coincide con algún tiempo creado y no es posible iniciar el tiempo."]);

            return;
        }

        // Comprueba si hay algún tiempo abierto que coincide con el rango de tiempo a crear.
        checkTime = await context
            .TimeControls
            .AnyAsync(
                tc => tc.UserId == timeControl.UserId && tc.TimeState == TimeState.Open &&
                      timeControl.Start >= tc.Start && timeControl.Start <= dateTimeService.UtcNow,
                cancellationToken);

        if (!checkTime)
        {
            return;
        }

        var message = localizer["El tiempo coincide con algún tiempo abierto y no es posible iniciar el tiempo."];
        validationFailureService.Add(ValidationErrorsKeys.NotificationErrors, message);
    }

    public async Task ValidateForUpdateAsync(TimeControl timeControl, CancellationToken cancellationToken)
    {
        // Comprueba si existe algún tiempo en el rango del tiempo a actualizar.
        var checkTime = await context
            .TimeControls
            .AnyAsync(
                tc =>
                    (tc.UserId == timeControl.UserId && tc.Id != timeControl.Id &&
                     timeControl.Start >= tc.Start && timeControl.Start <= tc.Finish) ||
                    (tc.UserId == timeControl.UserId && tc.Id != timeControl.Id &&
                     timeControl.Finish >= tc.Start && timeControl.Finish <= tc.Finish),
                cancellationToken);

        if (checkTime)
        {
            validationFailureService.Add(
                ValidationErrorsKeys.NotificationErrors,
                localizer["El tiempo coincide con algún tiempo creado y no es posible actualizar el tiempo."]);

            return;
        }

        // Comprueba si hay algún tiempo abierto que coincide con el rango de tiempo a actualizar.
        checkTime = await context
            .TimeControls
            .AnyAsync(
                tc => (tc.UserId == timeControl.UserId && tc.TimeState == TimeState.Open &&
                       timeControl.Start >= tc.Start && timeControl.Start <= dateTimeService.UtcNow) ||
                      (tc.UserId == timeControl.UserId && tc.TimeState == TimeState.Open &&
                       timeControl.Finish >= tc.Start && timeControl.Finish <= dateTimeService.UtcNow),
                cancellationToken);

        if (!checkTime)
        {
            return;
        }

        var message = localizer["El tiempo coincide con algún tiempo abierto y no es posible actualizar el tiempo."];
        validationFailureService.Add(ValidationErrorsKeys.NotificationErrors, message);
    }
}
