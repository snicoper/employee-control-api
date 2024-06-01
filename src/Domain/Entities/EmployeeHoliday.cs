using EmployeeControl.Domain.Common;

namespace EmployeeControl.Domain.Entities;

public class EmployeeHoliday : BaseAuditableEntity
{
    public int Year { get; set; }

    public int TotalDays { get; set; }

    public int ConsumedDays { get; set; }

    public Guid UserId { get; set; }

    public User User { get; set; } = null!;
}
