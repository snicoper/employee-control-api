using EmployeeControl.Domain.Common;

namespace EmployeeControl.Domain.Entities;

public class Company : BaseAuditableEntity
{
    public string Name { get; set; } = default!;

    public ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();

    public ICollection<CompanyTask> CompanyTasks { get; set; } = new List<CompanyTask>();
}
