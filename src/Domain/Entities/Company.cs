using EmployeeControl.Domain.Common;

namespace EmployeeControl.Domain.Entities;

public class Company : BaseAuditableEntity
{
    public string Name { get; set; } = default!;

    public CompanySettings CompanySettings { get; set; } = null!;

    public ICollection<Department> Departaments { get; set; } = new List<Department>();

    public ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();

    public ICollection<CompanyTask> CompanyTasks { get; set; } = new List<CompanyTask>();
}
