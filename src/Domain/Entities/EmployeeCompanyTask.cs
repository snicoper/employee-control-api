using EmployeeControl.Domain.Common;

namespace EmployeeControl.Domain.Entities;

/// <summary>
/// Asociación de empleados y tareas de la compañía.
/// </summary>
public class EmployeeCompanyTask : BaseAuditableEntity
{
    public Guid UserId { get; set; }

    public User User { get; set; } = null!;

    public Guid CompanyTaskId { get; set; }

    public CompanyTask CompanyTask { get; set; } = null!;
}
