using EmployeeControl.Domain.Common;

namespace EmployeeControl.Domain.Entities;

public class EmployeeCompanyTask : BaseAuditableEntity
{
    public Guid UserId { get; set; }

    public User User { get; set; } = null!;

    public Guid CompanyTaskId { get; set; }

    public CompanyTask CompanyTask { get; set; } = null!;
}
