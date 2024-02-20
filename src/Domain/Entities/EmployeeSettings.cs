using EmployeeControl.Domain.Common;

namespace EmployeeControl.Domain.Entities;

public class EmployeeSettings : BaseAuditableEntity
{
    public string UserId { get; set; } = default!;

    public ApplicationUser User { get; set; } = null!;
}
