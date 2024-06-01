using EmployeeControl.Domain.Common;

namespace EmployeeControl.Domain.Entities;

public class Department : BaseAuditableEntity
{
    public string Name { get; set; } = default!;

    public bool Active { get; set; }

    public string Background { get; set; } = default!;

    public string Color { get; set; } = default!;

    public ICollection<EmployeeDepartment> EmployeeDepartments { get; set; } = [];
}
