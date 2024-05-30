using EmployeeControl.Application.Common.Extensions;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Localization;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Entities;
using EmployeeControl.Domain.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace EmployeeControl.Infrastructure.Validators;

public class CompanyCalendarValidator(
    ILogger<CompanyCalendarValidator> logger,
    IStringLocalizer<CalendarResource> localizer,
    IApplicationDbContext context)
    : ICompanyCalendarValidator
{
    public async Task CreateValidationAsync(CompanyCalendar companyCalendar, CancellationToken cancellationToken)
    {
        var calendar = await context
            .CompanyCalendars
            .AnyAsync(
                cc => string.Equals(cc.Name.ToLower(), companyCalendar.Name.ToLower()),
                cancellationToken);

        if (!calendar)
        {
            return;
        }

        var errorMessage = localizer["El nombre del calendario ya existe."];
        logger.LogDebug("{Message}", errorMessage);

        Result
            .Failure(nameof(CompanyCalendar.Name), errorMessage)
            .RaiseBadRequestIfErrorsExist();
    }

    public async Task UpdateValidationAsync(CompanyCalendar companyCalendar, CancellationToken cancellationToken)
    {
        var calendar = await context
            .CompanyCalendars
            .AnyAsync(
                cc => cc.Id != companyCalendar.Id && string.Equals(cc.Name.ToLower(), companyCalendar.Name.ToLower()),
                cancellationToken);

        if (!calendar)
        {
            return;
        }

        var errorMessage = localizer["El nombre del calendario ya existe."];
        logger.LogDebug("{Message}", errorMessage);

        Result
            .Failure(nameof(CompanyCalendar.Name), errorMessage)
            .RaiseBadRequestIfErrorsExist();
    }
}
