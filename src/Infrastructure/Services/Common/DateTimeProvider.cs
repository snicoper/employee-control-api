﻿using EmployeeControl.Application.Common.Interfaces.Common;

namespace EmployeeControl.Infrastructure.Services.Common;

public class DateTimeProvider(TimeProvider timeProvider) : IDateTimeProvider
{
    public TimeProvider TimeProvider { get; } = timeProvider;

    public DateTimeOffset UtcNow => TimeProvider.GetUtcNow();

    public string TryConvertWindowsIdToIanaId(string windowsId)
    {
        if (!TimeZoneInfo.TryConvertWindowsIdToIanaId(windowsId, out var ianaId))
        {
            throw new TimeZoneNotFoundException($"No Iana time zone found for {windowsId}.");
        }

        return ianaId;
    }

    public string TryConvertIanaIdToWindowsId(string ianaId)
    {
        if (!TimeZoneInfo.TryConvertIanaIdToWindowsId(ianaId, out var windowsId))
        {
            throw new TimeZoneNotFoundException($"No Windows time zone found for {ianaId}.");
        }

        return windowsId;
    }

    public DateTimeOffset EndOfDay(DateTimeOffset dateTimeOffset)
    {
        var offset = TimeProvider.LocalTimeZone.GetUtcOffset(DateTimeOffset.UtcNow).TotalMinutes;
        var dateTime = dateTimeOffset.Date.AddHours(23).AddMinutes(59).AddMinutes(offset * -1).AddSeconds(59);
        var result = new DateTimeOffset(dateTime, TimeSpan.Zero);

        return result;
    }

    public DateTimeOffset StartOfDay(DateTimeOffset dateTimeOffset)
    {
        var offset = TimeProvider.LocalTimeZone.GetUtcOffset(DateTimeOffset.UtcNow).TotalMinutes;
        var dateTime = dateTimeOffset.Date.AddHours(00).AddMinutes(00).AddMinutes(offset * -1).AddSeconds(00);
        var result = new DateTimeOffset(dateTime, TimeSpan.Zero);

        return result;
    }
}
