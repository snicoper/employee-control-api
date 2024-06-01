using System.Text.Json;

namespace EmployeeControl.Application.Common.Serializers;

public static class CustomJsonSerializerOptions
{
    public static JsonSerializerOptions Default()
    {
        return new JsonSerializerOptions { WriteIndented = true, PropertyNameCaseInsensitive = true };
    }
}
