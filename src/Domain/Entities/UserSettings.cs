using EmployeeControl.Domain.Common;

namespace EmployeeControl.Domain.Entities;

public class UserSettings : BaseAuditableEntity
{
    public string UserId { get; set; } = default!;

    public ApplicationUser User { get; set; } = null!;
}
