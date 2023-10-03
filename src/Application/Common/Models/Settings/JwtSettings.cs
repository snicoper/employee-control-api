using System.ComponentModel.DataAnnotations;

namespace EmployeeControl.Application.Common.Models.Settings;

public class JwtSettings
{
    public const string SectionName = "Jwt";

    public const string JwtTokenLifeTimeMinutes = "Jwt:TokenLifeTimeMinutes";
    public const string JwtRefreshTokenLifeTimeDays = "Jwt:RefreshTokenLifeTimeDays";
    public const string JwtIssuer = "Jwt:Issuer";
    public const string JwtAudience = "Jwt:Audience";
    public const string JwtKey = "Jwt:Key";

    [Range(10, int.MaxValue)]
    public int TokenLifeTimeMinutes { get; set; }

    [Range(1, int.MaxValue)]
    public int RefreshTokenLifeTimeDays { get; set; }

    [Required]
    public string? Issuer { get; set; }

    [Required]
    public string? Audience { get; set; }

    [MinLength(32)]
    public string? Key { get; set; }
}
