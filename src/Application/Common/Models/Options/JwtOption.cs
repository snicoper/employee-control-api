using System.ComponentModel.DataAnnotations;

namespace EmployeeControl.Application.Common.Models.Options;

public class JwtOption
{
    public const string Section = "Jwt";
    public const string JwtIssuer = "Jwt:Issuer";
    public const string JwtAudience = "Jwt:Audience";
    public const string JwtKey = "Jwt:Key";

    [Required]
    public string? Issuer { get; set; }

    [Required]
    public string? Audience { get; set; }

    [MinLength(32)]
    public string? Key { get; set; }
}
