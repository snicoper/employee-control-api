using EmployeeControl.Domain.Common;

namespace EmployeeControl.Domain.Entities;

public class EmployeeHoliday : BaseAuditableEntity
{
    public int Year { get; set; }

    public int TotalDays { get; set; }

    public int Consumed { get; set; }

    public int Claimed { get; set; }

    public string UserId { get; set; } = default!;

    public ApplicationUser User { get; set; } = null!;
}
