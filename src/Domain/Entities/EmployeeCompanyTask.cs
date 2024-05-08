using EmployeeControl.Domain.Common;

namespace EmployeeControl.Domain.Entities;

/// <summary>
/// Asociación de empleados y tareas de la compañía.
/// </summary>
public class EmployeeCompanyTask : BaseAuditableEntity
{
    public string UserId { get; set; } = default!;

    public User User { get; set; } = null!;

    public string CompanyTaskId { get; set; } = default!;

    public CompanyTask CompanyTask { get; set; } = null!;
}
