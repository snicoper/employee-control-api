using EmployeeControl.Domain.Common;

namespace EmployeeControl.Domain.Entities;

public class EmployeeSettings : BaseAuditableEntity
{
    public string Timezone { get; set; } = default!;

    public string UserId { get; set; } = default!;

    public ApplicationUser User { get; set; } = null!;
}
