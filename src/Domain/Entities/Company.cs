using EmployeeControl.Domain.Common;

namespace EmployeeControl.Domain.Entities;

/// <summary>
/// Compañías registradas.
/// </summary>
public class Company : BaseAuditableEntity
{
    public string Name { get; set; } = default!;

    public CompanySettings CompanySettings { get; set; } = null!;

    public WorkingDaysWeek WorkingDaysWeek { get; set; } = null!;

    public ICollection<Department> Departaments { get; set; } = new List<Department>();

    public ICollection<User> Users { get; set; } = new List<User>();

    public ICollection<CompanyTask> CompanyTasks { get; set; } = new List<CompanyTask>();

    public ICollection<CategoryAbsence> CategoryAbsences { get; set; } = new List<CategoryAbsence>();
}
