using System.Text.Json;

namespace EmployeeControl.Application.Common.Serializers;

public static class CustomJsonSerializerOptions
{
    /// <summary>
    /// Opciones por defecto para <see cref="System.Text.Json" />.
    /// <para>
    /// WriteIndented = true,
    /// PropertyNameCaseInsensitive = true.
    /// </para>
    /// </summary>
    /// <returns><see cref="JsonSerializerOptions" />.</returns>
    public static JsonSerializerOptions Default()
    {
        return new JsonSerializerOptions { WriteIndented = true, PropertyNameCaseInsensitive = true };
    }
}
