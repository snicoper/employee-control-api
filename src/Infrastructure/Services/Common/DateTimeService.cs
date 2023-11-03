using System.Runtime.InteropServices;
using EmployeeControl.Application.Common.Exceptions;
using EmployeeControl.Application.Common.Interfaces.Common;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace EmployeeControl.Infrastructure.Services.Common;

public class DateTimeService : IDateTimeService
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly ILogger<DateTimeService> _logger;

    public DateTimeService(
        IApplicationDbContext context,
        ICurrentUserService currentUserService,
        TimeProvider timeProvider,
        ILogger<DateTimeService> logger)
    {
        _context = context;
        _currentUserService = currentUserService;
        _logger = logger;
        TimeProvider = timeProvider;
        CompanyTimezone = GetIanaTimezoneCompany();
    }

    public string CompanyTimezone { get; }

    public TimeProvider TimeProvider { get; }

    public DateTimeOffset UtcNow => TimeProvider.GetUtcNow();

    public string TryConvertWindowsIdToIanaId(string windowsId)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            _logger.LogWarning("Try to obtain a time zone {windowsId} on Windows platform.", windowsId);
        }

        if (!TimeZoneInfo.TryConvertWindowsIdToIanaId(windowsId, out var ianaId))
        {
            throw new TimeZoneNotFoundException($"No Iana time zone found for {windowsId}.");
        }

        return ianaId;
    }

    public string TryConvertIanaIdToWindowsId(string ianaId)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            _logger.LogWarning("Try to obtain a time zone {ianaId} on Linux platform.", ianaId);
        }

        if (!TimeZoneInfo.TryConvertIanaIdToWindowsId(CompanyTimezone, out var windowsId))
        {
            throw new TimeZoneNotFoundException($"No Windows time zone found for {CompanyTimezone}.");
        }

        return windowsId;
    }

    public DateTimeOffset ConvertToTimezoneCompany(DateTimeOffset datetime)
    {
        var timezoneId = CompanyTimezone;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            timezoneId = TryConvertIanaIdToWindowsId(CompanyTimezone);
        }

        var datetimeZone = TimeZoneInfo.ConvertTime(datetime, TimeZoneInfo.FindSystemTimeZoneById(timezoneId));

        return datetimeZone;
    }

    public DateTimeOffset EndOfDay(DateTimeOffset dateTimeOffset)
    {
        var offset = TimeProvider.LocalTimeZone.GetUtcOffset(DateTimeOffset.UtcNow).TotalMinutes;
        var datetime = dateTimeOffset.Date.AddHours(23).AddMinutes(59).AddMinutes(offset * -1).AddSeconds(59);
        var result = new DateTimeOffset(datetime, TimeSpan.Zero);

        return result;
    }

    public DateTimeOffset StartOfDay(DateTimeOffset dateTimeOffset)
    {
        var offset = TimeProvider.LocalTimeZone.GetUtcOffset(DateTimeOffset.UtcNow).TotalMinutes;
        var datetime = dateTimeOffset.Date.AddHours(00).AddMinutes(00).AddMinutes(offset * -1).AddSeconds(00);
        var result = new DateTimeOffset(datetime, TimeSpan.Zero);

        return result;
    }

    private string GetIanaTimezoneCompany()
    {
        var timezoneId = TimeZoneInfo.Local.Id;

        if (!string.IsNullOrEmpty(_currentUserService.CompanyId))
        {
            var companySettings = _context
                                      .CompanySettings
                                      .SingleOrDefault(cs => cs.CompanyId == _currentUserService.CompanyId) ??
                                  throw new NotFoundException(nameof(CompanySettings), nameof(CompanySettings.Id));

            timezoneId = !string.IsNullOrEmpty(companySettings.Timezone) ? companySettings.Timezone : TimeZoneInfo.Local.Id;
        }

        var resultTimezoneId = timezoneId;

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            resultTimezoneId = TryConvertWindowsIdToIanaId(timezoneId);
        }

        return resultTimezoneId;
    }
}
