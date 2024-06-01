using EmployeeControl.Domain.Common;

namespace EmployeeControl.Domain.Entities;

public class Company : BaseAuditableEntity
{
    public string Name { get; set; } = default!;

    public CompanySettings CompanySettings { get; set; } = null!;

    public WorkingDaysWeek WorkingDaysWeek { get; set; } = null!;

    public ICollection<Department> Departments { get; set; } = [];

    public ICollection<User> Users { get; set; } = [];

    public ICollection<CompanyTask> CompanyTasks { get; set; } = [];

    public ICollection<CategoryAbsence> CategoryAbsences { get; set; } = [];
}
