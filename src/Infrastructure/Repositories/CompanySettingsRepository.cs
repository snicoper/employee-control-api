using System.Runtime.InteropServices;
using EmployeeControl.Application.Common.Interfaces.Common;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Users;
using EmployeeControl.Domain.Entities;
using EmployeeControl.Domain.Exceptions;
using EmployeeControl.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl.Infrastructure.Repositories;

public class CompanySettingsRepository(
    IApplicationDbContext context,
    ICurrentUserService currentUserService,
    IDateTimeProvider dateTimeProvider)
    : ICompanySettingsRepository
{
    public async Task<CompanySettings> GetByIdAsync(Guid companySettingsId, CancellationToken cancellationToken)
    {
        var result = await context
            .CompanySettings
            .SingleOrDefaultAsync(cs => cs.Id == companySettingsId, cancellationToken)
                ?? throw new NotFoundException(nameof(CompanySettings), nameof(CompanySettings.Id));

        return result;
    }

    public async Task<CompanySettings> GetCompanySettingsAsync(CancellationToken cancellationToken)
    {
        var result = await context
            .CompanySettings
            .FirstOrDefaultAsync(cancellationToken)
                ?? throw new NotFoundException(nameof(CompanySettings), nameof(CompanySettings.Id));

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
        DateTimeOffset dateTime,
        CancellationToken cancellationToken)
    {
        var timezoneId = await GetIanaTimezoneCompanyAsync(currentUserService.CompanyId, cancellationToken);
        var dateTimeZone = TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.FindSystemTimeZoneById(timezoneId));

        return dateTimeZone;
    }

    public async Task<string> GetIanaTimezoneCompanyAsync(Guid companyId, CancellationToken cancellationToken)
    {
        var companySettings = await GetCompanySettingsAsync(cancellationToken);
        var timezoneId = !string.IsNullOrEmpty(companySettings.Timezone)
            ? companySettings.Timezone
            : TimeZoneInfo.Local.Id;

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            timezoneId = dateTimeProvider.TryConvertIanaIdToWindowsId(timezoneId);
        }

        return timezoneId;
    }
}
