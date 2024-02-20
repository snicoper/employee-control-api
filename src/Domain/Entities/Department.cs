using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Interfaces;

namespace EmployeeControl.Domain.Entities;

public class Department : BaseAuditableEntity, ICompany
{
    public string Name { get; set; } = default!;

    public bool Active { get; set; }

    public string Background { get; set; } = default!;

    public string Color { get; set; } = default!;

    public ICollection<EmployeeDepartment> UserDepartments { get; set; } = new List<EmployeeDepartment>();

    public string CompanyId { get; set; } = default!;

    public Company Company { get; set; } = null!;
}
