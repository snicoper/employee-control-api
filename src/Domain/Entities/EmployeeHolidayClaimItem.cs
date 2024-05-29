using EmployeeControl.Domain.Common;

namespace EmployeeControl.Domain.Entities;

/// <summary>
/// Días reclamados para vacaciones.
/// </summary>
public class EmployeeHolidayClaimItem : BaseAuditableEntity
{
    public DateOnly Date { get; set; }

    public Guid UserId { get; set; }

    public User User { get; set; } = null!;

    public Guid EmployeeHolidayClaimId { get; set; }

    public EmployeeHolidayClaim EmployeeHolidayClaim { get; set; } = null!;
}
