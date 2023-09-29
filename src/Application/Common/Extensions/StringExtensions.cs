﻿namespace EmployeeControl.Application.Common.Extensions;

public static class StringExtensions
{
    /// <summary>
    ///     Devolver un string con la primera letra en minúscula.
    /// </summary>
    public static string LowerCaseFirst(this string str)
    {
        return string.IsNullOrEmpty(str) ? str : $"{str[..1].ToLower()}{str[1..]}";
    }

    /// <summary>
    ///     Devolver un string con la primera letra en mayúscula.
    /// </summary>
    public static string UpperCaseFirst(this string str)
    {
        return string.IsNullOrEmpty(str) ? str : $"{str[..1].ToUpper()}{str[1..]}";
    }

    /// <summary>
    ///     Devolver un string con la primera letra de cada palabra en mayúscula.
    /// </summary>
    public static string ToTile(this string str)
    {
        var parts = str.Split()
            .Select(part => part.UpperCaseFirst())
            .ToList();

        return string.IsNullOrEmpty(str) ? str : string.Join(" ", parts);
    }
}
