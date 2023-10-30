using EmployeeControl.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace EmployeeControl.Domain.Entities;

public class ApplicationUser : IdentityUser, ICompany
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? RefreshToken { get; set; }

    public bool Active { get; set; }

    public DateTimeOffset? RefreshTokenExpiryTime { get; set; }

    public DateTimeOffset? EntryDate { get; set; }

    public ICollection<UserCompanyTask> UserCompanyTasks { get; set; } = new List<UserCompanyTask>();

    public ICollection<TimeControl> TimeControls { get; set; } = new List<TimeControl>();

    public Company? Company { get; set; }

    public string CompanyId { get; set; } = default!;
}
