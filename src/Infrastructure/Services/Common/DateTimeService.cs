using EmployeeControl.Application.Common.Exceptions;
using EmployeeControl.Application.Common.Interfaces.Common;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Infrastructure.Services.Common;

public class DateTimeService : IDateTimeService
{
    public DateTimeService(IApplicationDbContext context, ICurrentUserService currentUserService, TimeProvider timeProvider)
    {
        TimeProvider = timeProvider;

        var companySettings = context
                                  .CompanySettings
                                  .SingleOrDefault(cs => cs.CompanyId == currentUserService.CompanyId) ??
                              throw new NotFoundException(nameof(CompanySettings), nameof(CompanySettings.Id));

        CompanyTimezone = companySettings.Timezone ?? throw new ArgumentNullException(nameof(CompanySettings.Timezone));
    }

    public string CompanyTimezone { get; }

    public TimeProvider TimeProvider { get; }

    public DateTimeOffset UtcNow => TimeProvider.GetUtcNow();

    public DateTimeOffset ConvertToTimezoneCompany(DateTimeOffset datetime)
    {
        if (!TimeZoneInfo.TryConvertIanaIdToWindowsId(CompanyTimezone, out var timeZoneInfo))
        {
            throw new TimeZoneNotFoundException($"No Windows time zone found for {CompanyTimezone}.");
        }

        var datetimeZone = TimeZoneInfo.ConvertTime(datetime, TimeZoneInfo.FindSystemTimeZoneById(timeZoneInfo));

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
}
