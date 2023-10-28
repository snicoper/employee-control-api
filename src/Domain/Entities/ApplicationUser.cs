using EmployeeControl.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace EmployeeControl.Domain.Entities;

public class ApplicationUser : IdentityUser, ICompanyId
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? RefreshToken { get; set; }

    public bool Active { get; set; }

    public DateTimeOffset? RefreshTokenExpiryTime { get; set; }

    public DateTimeOffset? EntryDate { get; set; }

    public Company? Company { get; set; }

    public ICollection<UserCompanyTask> UserCompanyTasks { get; set; } = new List<UserCompanyTask>();

    public string CompanyId { get; set; } = default!;
}
