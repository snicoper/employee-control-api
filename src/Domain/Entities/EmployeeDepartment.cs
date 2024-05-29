using EmployeeControl.Domain.Common;

namespace EmployeeControl.Domain.Entities;

/// <summary>
/// Asociación de empleados y departamentos.
/// </summary>
public class EmployeeDepartment : BaseAuditableEntity
{
    public Guid DepartmentId { get; set; }

    public Department Department { get; set; } = null!;

    public Guid UserId { get; set; } = default!;

    public User User { get; set; } = null!;
}
