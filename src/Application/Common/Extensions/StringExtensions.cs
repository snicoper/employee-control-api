using EmployeeControl.Application.Common.Exceptions;

namespace EmployeeControl.Application.Common.Extensions;

public static class StringExtensions
{
    /// <summary>
    ///     Lanza una excepción si el valor es vacío o nulo.
    /// </summary>
    /// <param name="value">El valor de comprobar.</param>
    /// <returns>Retorna el mismo valor pasado por parámetro.</returns>
    /// <exception cref="ConfigurationNullParameterException"></exception>
    public static string RaiseConfigurationNullParameterExceptionIfNullOrEmpty(this string? value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ConfigurationNullParameterException($"The parameter {nameof(value)} cannot be null");
        }


        return value;
    }

    /// <summary>
    ///     Devolver un string con la primera letra en minúscula.
    /// </summary>
    public static string LowerCaseFirst(this string value)
    {
        return string.IsNullOrEmpty(value) ? value : $"{value[..1].ToLower()}{value[1..]}";
    }

    /// <summary>
    ///     Devolver un string con la primera letra en mayúscula.
    /// </summary>
    public static string UpperCaseFirst(this string value)
    {
        return string.IsNullOrEmpty(value) ? value : $"{value[..1].ToUpper()}{value[1..]}";
    }

    /// <summary>
    ///     Devolver un string con la primera letra de cada palabra en mayúscula.
    /// </summary>
    public static string ToTile(this string value)
    {
        var parts = value.Split()
            .Select(part => part.UpperCaseFirst())
            .ToList();

        return string.IsNullOrEmpty(value) ? value : string.Join(" ", parts);
    }
}
