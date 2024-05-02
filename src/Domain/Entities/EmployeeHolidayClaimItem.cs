using EmployeeControl.Domain.Common;

namespace EmployeeControl.Domain.Entities;

/// <summary>
/// Días reclamados para vacaciones.
/// </summary>
public class EmployeeHolidayClaimItem : BaseAuditableEntity
{
    public DateOnly Date { get; set; }

    public string UserId { get; set; } = default!;

    public ApplicationUser User { get; set; } = null!;

    public string EmployeeHolidayClaimId { get; set; } = null!;

    public EmployeeHolidayClaim EmployeeHolidayClaim { get; set; } = null!;
}
