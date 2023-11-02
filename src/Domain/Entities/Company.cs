using EmployeeControl.Domain.Common;

namespace EmployeeControl.Domain.Entities;

public class Company : BaseAuditableEntity
{
    public Company()
    {
        Users = new List<ApplicationUser>();
        CompanyTasks = new List<CompanyTask>();

        CompanySettings = new CompanySettings();
        CompanySettingsId = CompanySettings.Id;
    }

    public string Name { get; set; } = default!;

    public string? CompanySettingsId { get; set; }

    public CompanySettings CompanySettings { get; }

    public ICollection<ApplicationUser> Users { get; set; }

    public ICollection<CompanyTask> CompanyTasks { get; set; }
}
