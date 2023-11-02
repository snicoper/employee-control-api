namespace EmployeeControl.Application.Common.Extensions;

public static class DateTimeOffsetExtensions
{
    /// <summary>
    /// A una fecha dada, establece la hora al final del día de la fecha.
    /// </summary>
    /// <param name="dateTimeOffset">Fecha a establecer el final del día.</param>
    /// <param name="timeProvider"><see cref="TimeProvider" />.</param>
    /// <returns>Un <see cref="DateTimeOffset" /> con la hora final del día.</returns>
    public static DateTimeOffset EndOfDay(this DateTimeOffset dateTimeOffset, TimeProvider timeProvider)
    {
        var offset = timeProvider.LocalTimeZone.GetUtcOffset(DateTimeOffset.UtcNow).TotalMinutes;
        var datetime = dateTimeOffset.Date.AddHours(23).AddMinutes(59).AddMinutes(offset * -1).AddSeconds(59);
        var result = new DateTimeOffset(datetime, TimeSpan.Zero);

        return result;
    }

    /// <summary>
    /// A una fecha dada, establece la hora al inicio del día de la fecha.
    /// </summary>
    /// <param name="dateTimeOffset">Fecha a establecer el inicio del día.</param>
    /// <param name="timeProvider"><see cref="TimeProvider" />.</param>
    /// <returns>Un <see cref="DateTimeOffset" /> con la hora inicial del día.</returns>
    public static DateTimeOffset StartOfDay(this DateTimeOffset dateTimeOffset, TimeProvider timeProvider)
    {
        var offset = timeProvider.LocalTimeZone.GetUtcOffset(DateTimeOffset.UtcNow).TotalMinutes;
        var datetime = dateTimeOffset.Date.AddHours(00).AddMinutes(00).AddMinutes(offset * -1).AddSeconds(00);
        var result = new DateTimeOffset(datetime, TimeSpan.Zero);

        return result;
    }
}
