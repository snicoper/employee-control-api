using EmployeeControl.Domain.Common;

namespace EmployeeControl.Domain.Entities;

/// <summary>
/// Configuración de empleados.
/// </summary>
public class EmployeeSettings : BaseAuditableEntity
{
    public string Timezone { get; set; } = default!;

    public Guid UserId { get; set; }

    public User User { get; set; } = null!;
}
