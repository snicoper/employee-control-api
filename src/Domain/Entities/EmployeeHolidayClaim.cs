using EmployeeControl.Domain.Common;

namespace EmployeeControl.Domain.Entities;

public class EmployeeHolidayClaim : BaseAuditableEntity
{
    public int Year { get; set; }

    public string? Description { get; set; }

    public bool Accepted { get; set; }

    public Guid UserId { get; set; }

    public User User { get; set; } = null!;

    public ICollection<EmployeeHolidayClaimItem> EmployeeHolidayClaimLines { get; set; } = [];
}
