﻿using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Features.CompanyCalendars;
using EmployeeControl.Application.Common.Localization;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace EmployeeControl.Infrastructure.Services.Features.CompanyCalendars;

public class CompanyCalendarValidatorService(
    ILogger<CompanyCalendarValidatorService> logger,
    IStringLocalizer<CalendarResource> localizer,
    IApplicationDbContext context)
    : ICompanyCalendarValidatorService
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

        var messageError = localizer["El nombre del calendario ya existe."];
        logger.LogDebug("{Message}", messageError);
        Result.Failure(nameof(CompanyCalendar.Name), messageError).RaiseBadRequest();
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

        var messageError = localizer["El nombre del calendario ya existe."];
        logger.LogDebug("{Message}", messageError);
        Result.Failure(nameof(CompanyCalendar.Name), messageError).RaiseBadRequest();
    }
}
