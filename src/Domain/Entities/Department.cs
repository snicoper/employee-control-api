using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Interfaces;

namespace EmployeeControl.Domain.Entities;

public class Department : BaseAuditableEntity, ICompany
{
    public string Name { get; set; } = default!;

    public bool Active { get; set; }

    public ICollection<UserDepartment> UserDepartments { get; set; } = new List<UserDepartment>();

    public string CompanyId { get; set; } = default!;

    public Company Company { get; set; } = null!;
}
