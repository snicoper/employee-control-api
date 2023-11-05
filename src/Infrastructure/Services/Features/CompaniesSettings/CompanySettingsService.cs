﻿using System.Runtime.InteropServices;
using EmployeeControl.Application.Common.Exceptions;
using EmployeeControl.Application.Common.Interfaces.Common;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Features.CompaniesSettings;
using EmployeeControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl.Infrastructure.Services.Features.CompaniesSettings;

public class CompanySettingsService(
    IApplicationDbContext context,
    ICurrentUserService currentUserService,
    IDateTimeService dateTimeService)
    : ICompanySettingsService
{
    public async Task<CompanySettings> GatByIdAsync(string companySettingsId, CancellationToken cancellationToken)
    {
        var result = await context
                         .CompanySettings
                         .AsNoTracking()
                         .SingleOrDefaultAsync(cs => cs.Id == companySettingsId, cancellationToken) ??
                     throw new NotFoundException(nameof(CompanySettings), nameof(CompanySettings.CompanyId));

        return result;
    }

    public async Task<CompanySettings> GetByCompanyIdAsync(string companyId, CancellationToken cancellationToken)
    {
        var result = await context
                         .CompanySettings
                         .AsNoTracking()
                         .SingleOrDefaultAsync(cs => cs.CompanyId == companyId, cancellationToken) ??
                     throw new NotFoundException(nameof(CompanySettings), nameof(CompanySettings.CompanyId));

        return result;
    }

    public async Task<CompanySettings> CreateAsync(CompanySettings companySettings, CancellationToken cancellationToken)
    {
        await context.CompanySettings.AddAsync(companySettings, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return companySettings;
    }

    public async Task<CompanySettings> UpdateAsync(CompanySettings companySettings, CancellationToken cancellationToken)
    {
        context.CompanySettings.Update(companySettings);
        await context.SaveChangesAsync(cancellationToken);

        return companySettings;
    }

    public async Task<DateTimeOffset> ConvertToTimezoneCurrentCompanyAsync(
        DateTimeOffset datetime,
        CancellationToken cancellationToken)
    {
        var timezoneId = await GetIanaTimezoneCompanyAsync(currentUserService.CompanyId, cancellationToken);
        var datetimeZone = TimeZoneInfo.ConvertTime(datetime, TimeZoneInfo.FindSystemTimeZoneById(timezoneId));

        return datetimeZone;
    }

    public async Task<string> GetIanaTimezoneCompanyAsync(string companyId, CancellationToken cancellationToken)
    {
        var companySettings = await GetByCompanyIdAsync(companyId, cancellationToken);
        var timezoneId = !string.IsNullOrEmpty(companySettings.Timezone) ? companySettings.Timezone : TimeZoneInfo.Local.Id;

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            timezoneId = dateTimeService.TryConvertIanaIdToWindowsId(timezoneId);
        }

        return timezoneId;
    }
}
