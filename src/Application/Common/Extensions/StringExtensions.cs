using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace EmployeeControl.Application.Common.Extensions;

public static class StringExtensions
{
    /// <summary>
    /// Si un string es null, devolverá string.Empty.
    /// </summary>
    /// <param name="value">String a comprobar.</param>
    /// <returns>Si el string es null devolverá string.Empty, en caso contrario el valor de value.</returns>
    public static string SetEmptyIfNull(this string? value)
    {
        return value ?? string.Empty;
    }

    /// <summary>
    /// Comprueba si un string es null.
    /// </summary>
    /// <param name="value">String a comprobar.</param>
    /// <returns>True si es null, false en caso contrario.</returns>
    public static bool IsNull(this string? value)
    {
        return value is null;
    }

    /// <summary>
    /// Devolver un string con la primera letra en minúscula.
    /// </summary>
    /// <param name="value">String a convertir.</param>
    /// <returns>El string con la primera letra en minúsculas.</returns>
    public static string LowerCaseFirst(this string value)
    {
        return string.IsNullOrEmpty(value) ? value : $"{value[..1].ToLower()}{value[1..]}";
    }

    /// <summary>
    /// Devolver un string con la primera letra en mayúscula.
    /// </summary>
    /// <param name="value">String a convertir.</param>
    /// <returns>El string con la primera letra en mayúsculas.</returns>
    public static string UpperCaseFirst(this string value)
    {
        return string.IsNullOrEmpty(value) ? value : $"{value[..1].ToUpper()}{value[1..]}";
    }

    /// <summary>
    /// Devolver un string con la primera letra de cada palabra en mayúscula.
    /// </summary>
    /// <param name="value">String a convertir.</param>
    /// <returns>El string con la primera letra de casa palabra en mayúsculas.</returns>
    public static string ToTile(this string value)
    {
        var parts = value.Split()
            .Select(part => part.UpperCaseFirst())
            .ToList();

        return string.IsNullOrEmpty(value) ? value : string.Join(" ", parts);
    }

    /// <summary>
    /// Genera un Slug de un string.
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static string Slugify(this string text)
    {
        const string pattern = @"[^a-zA-Z0-9\-]";
        const string replacement = "-";
        var regex = new Regex(pattern);
        var result = regex
            .Replace(RemoveDiacritics(text), replacement)
            .Replace("--", "-")
            .Trim('-');

        return result;
    }

    private static string RemoveDiacritics(string text)
    {
        var normalizedString = text.ToLower().Trim().Normalize(NormalizationForm.FormD);

        var stringBuilder = new StringBuilder();
        foreach (var c in normalizedString)
        {
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
            {
                stringBuilder.Append(c);
            }
        }

        return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
    }
}
