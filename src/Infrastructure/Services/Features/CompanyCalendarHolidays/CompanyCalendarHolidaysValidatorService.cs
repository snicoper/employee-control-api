using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Features.CompanyCalendarHolidays;
using EmployeeControl.Application.Common.Localization;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace EmployeeControl.Infrastructure.Services.Features.CompanyCalendarHolidays;

public class CompanyCalendarHolidaysValidatorService(IApplicationDbContext context, IStringLocalizer<CalendarResource> localizer)
    : ICompanyCalendarHolidaysValidatorService
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
            var messageError = localizer["La fecha seleccionada ya tiene asignado un día festivo."];
            Result.Failure(nameof(CompanyCalendarHoliday.Date), messageError).RaiseBadRequest();
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
            var messageError = localizer["La descripción ya existe en el año {0}.", companyCalendarHoliday.Date.Year];
            Result.Failure(nameof(CompanyCalendarHoliday.Description), messageError).RaiseBadRequest();
        }
    }

    public async Task ValidateUpdateHolidayInDateAsync(
        CompanyCalendarHoliday companyCalendarHoliday,
        CancellationToken cancellationToken)
    {
        var dateExists = await context
            .CompanyCalendarHoliday
            .AnyAsync(ch => ch.Id != companyCalendarHoliday.Id && ch.Date == companyCalendarHoliday.Date, cancellationToken);

        if (!dateExists)
        {
            return;
        }

        var messageError = localizer["La fecha seleccionada ya tiene asignado un día festivo."];
        Result.Failure(nameof(CompanyCalendarHoliday.Date), messageError).RaiseBadRequest();
    }
}
