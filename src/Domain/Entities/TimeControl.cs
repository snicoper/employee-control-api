using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Enums;
using EmployeeControl.Domain.Interfaces;

namespace EmployeeControl.Domain.Entities;

public class TimeControl : BaseAuditableEntity, ICompany
{
    public TimeControl()
    {
        ClosedBy = ClosedBy.Unclosed;
        TimeState = TimeState.Open;
    }

    public DateTimeOffset Start { get; set; }

    public DateTimeOffset Finish { get; set; }

    public ClosedBy ClosedBy { get; set; }

    public TimeState TimeState { get; set; }

    public ApplicationUser? User { get; set; }

    public string UserId { get; set; } = default!;

    public Company? Company { get; set; }

    public string CompanyId { get; set; } = default!;
}
