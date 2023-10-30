using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Interfaces;

namespace EmployeeControl.Domain.Entities;

public class TimeControl : BaseAuditableEntity, ICompany
{
    public DateTimeOffset Start { get; set; }

    public DateTimeOffset? Finish { get; set; }

    public ApplicationUser? User { get; set; }

    public string UserId { get; set; } = default!;

    public Company? Company { get; set; }

    public string CompanyId { get; set; } = default!;
}
