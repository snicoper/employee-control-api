namespace EmployeeControl.Application.Common.Interfaces.Common;

public interface IDateTimeService
{
    /// <summary>
    /// Obtener el <see cref="TimeProvider" />.
    /// </summary>
    TimeProvider TimeProvider { get; }

    /// <summary>
    /// Obtener el Timezone de la configuración de la compañía.
    /// </summary>
    string CompanyTimezone { get; }

    /// <summary>
    /// Alias de TimeProvider.GetUtcNow().
    /// </summary>
    DateTimeOffset UtcNow { get; }

    /// <summary>
    /// Alias de <see cref="TimeZoneInfo.TryConvertWindowsIdToIanaId(string,string?,out string?)" />.
    /// </summary>
    /// <param name="windowsId">Timezone Windows.</param>
    /// <returns>Timezone Iana.</returns>
    string TryConvertWindowsIdToIanaId(string windowsId);

    /// <summary>
    /// Alias de <see cref="TimeZoneInfo.TryConvertIanaIdToWindowsId" />.
    /// </summary>
    /// <param name="ianaId">Timezone Iana.</param>
    /// <returns>Timezone Windows.</returns>
    string TryConvertIanaIdToWindowsId(string ianaId);

    /// <summary>
    /// Convierte un <see cref="DateTimeOffset" /> con la zona horaria de la compañía.
    /// </summary>
    /// <param name="datetime">Datetime a convertir.</param>
    /// <returns>Un <see cref="DateTimeOffset" /> con el offset en base al Timezone de la compañía.</returns>
    DateTimeOffset ConvertToTimezoneCompany(DateTimeOffset datetime);

    /// <summary>
    /// A una fecha dada, establece la hora al final del día de la fecha.
    /// </summary>
    /// <param name="dateTimeOffset">Fecha a establecer el final del día.</param>
    /// <returns>Un <see cref="DateTimeOffset" /> con la hora final del día.</returns>
    DateTimeOffset EndOfDay(DateTimeOffset dateTimeOffset);

    /// <summary>
    /// A una fecha dada, establece la hora al inicio del día de la fecha.
    /// </summary>
    /// <param name="dateTimeOffset">Fecha a establecer el inicio del día.</param>
    /// <returns>Un <see cref="DateTimeOffset" /> con la hora inicial del día.</returns>
    DateTimeOffset StartOfDay(DateTimeOffset dateTimeOffset);
}
