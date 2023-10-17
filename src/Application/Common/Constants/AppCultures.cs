using System.Globalization;

namespace EmployeeControl.Application.Common.Constants;

public static class AppCultures
{
    public static readonly CultureInfo Es = new("es");

    public static readonly CultureInfo EsEs = new("es-ES");

    public static readonly CultureInfo En = new("en");

    public static readonly CultureInfo EnUs = new("en-US");

    /// <summary>
    /// Cultura por defecto.
    /// </summary>
    public static readonly CultureInfo DefaultCulture = EsEs;

    /// <summary>
    /// Obtener todas las culturas disponibles.
    /// </summary>
    /// <returns>Lista CultureInfo disponibles.</returns>
    public static List<CultureInfo> GetAll()
    {
        return new List<CultureInfo> { Es, EsEs, En, EnUs };
    }
}
