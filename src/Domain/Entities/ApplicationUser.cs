using Microsoft.AspNetCore.Identity;

namespace EmployeeControl.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public int CompanyId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? RefreshToken { get; set; }

    public DateTimeOffset RefreshTokenExpiryTime { get; set; }

    public Company? Company { get; set; }
}
