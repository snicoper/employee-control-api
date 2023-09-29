namespace EmployeeControl.Application.Common.Extensions;

public static class StringExtensions
{
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
