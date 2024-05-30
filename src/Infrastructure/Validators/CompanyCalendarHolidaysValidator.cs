using EmployeeControl.Application.Common.Extensions;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Localization;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Entities;
using EmployeeControl.Domain.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace EmployeeControl.Infrastructure.Validators;

public class CompanyCalendarHolidaysValidator(IApplicationDbContext context, IStringLocalizer<CalendarResource> localizer)
    : ICompanyCalendarHolidaysValidator
{
    public async Task ValidateCreateHolidayInDateAsync(
        CompanyCalendarHoliday companyCalendarHoliday,
        CancellationToken cancellationToken)
    {
        // Comprueba si la fecha existe.
        var dateExists = await context
            .CompanyCalendarHoliday
            .AnyAsync(ch => ch.Date == companyCalendarHoliday.Date, cancellationToken);

        if (dateExists)
        {
            var errorMessage = localizer["La fecha seleccionada ya tiene asignado un día festivo."];

            Result
                .Failure(nameof(CompanyCalendarHoliday.Date), errorMessage)
                .RaiseBadRequestIfErrorsExist();
        }

        // Comprueba si la descripción ya existe en el mismo año.
        dateExists = await context
            .CompanyCalendarHoliday
            .AnyAsync(
                ch => ch.Date.Year == companyCalendarHoliday.Date.Year
                    && ch.Description == companyCalendarHoliday.Description,
                cancellationToken);

        if (dateExists)
        {
            var errorMessage = localizer["La descripción ya existe en el año {0}.", companyCalendarHoliday.Date.Year];

            Result
                .Failure(nameof(CompanyCalendarHoliday.Description), errorMessage)
                .RaiseBadRequestIfErrorsExist();
        }
    }

    public async Task ValidateUpdateHolidayInDateAsync(
        CompanyCalendarHoliday companyCalendarHoliday,
        CancellationToken cancellationToken)
    {
        var dateExists = await context
            .CompanyCalendarHoliday
            .AnyAsync(ch => ch.Id != companyCalendarHoliday.Id && ch.Date == companyCalendarHoliday.Date, cancellationToken);

        if (dateExists)
        {
            var errorMessage = localizer["La fecha seleccionada ya tiene asignado un día festivo."];

            Result
                .Failure(nameof(CompanyCalendarHoliday.Date), errorMessage)
                .RaiseBadRequestIfErrorsExist();
        }

        // Comprueba si la descripción ya existe en el mismo año.
        dateExists = await context
            .CompanyCalendarHoliday
            .AnyAsync(
                ch => ch.Date.Year == companyCalendarHoliday.Date.Year
                    && ch.Description == companyCalendarHoliday.Description
                    && ch.Id != companyCalendarHoliday.Id,
                cancellationToken);

        if (dateExists)
        {
            var errorMessage = localizer["La descripción ya existe en el año {0}.", companyCalendarHoliday.Date.Year];

            Result
                .Failure(nameof(CompanyCalendarHoliday.Description), errorMessage)
                .RaiseBadRequestIfErrorsExist();
        }
    }
}
