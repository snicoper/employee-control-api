using System.Globalization;

namespace EmployeeControl.Application.Common.Constants;

public static class AppCultures
{
    public static readonly CultureInfo DefaultCulture = new("es-ES");

    private static readonly CultureInfo Es = new("es");

    private static readonly CultureInfo EsEs = new("es-ES");

    private static readonly CultureInfo En = new("en");

    private static readonly CultureInfo EnUs = new("en-US");

    public static List<CultureInfo> GetAll()
    {
        return [Es, EsEs, En, EnUs];
    }
}
