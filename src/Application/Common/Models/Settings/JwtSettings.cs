using System.ComponentModel.DataAnnotations;

namespace EmployeeControl.Application.Common.Models.Settings;

public class JwtSettings
{
    public const string SectionName = "Jwt";

    public const string JwtLifeTimeDays = "Jwt:LifeTimeDays";
    public const string JwtIssuer = "Jwt:Issuer";
    public const string JwtAudience = "Jwt:Audience";
    public const string JwtKey = "Jwt:Key";

    [Range(10, int.MaxValue)]
    public int LifeTimeDays { get; set; }

    [Required]
    public string? Issuer { get; set; }

    [Required]
    public string? Audience { get; set; }

    [MinLength(32)]
    public string? Key { get; set; }
}
