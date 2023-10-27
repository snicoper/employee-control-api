using EmployeeControl.Domain.Common;

namespace EmployeeControl.Domain.Entities;

public class CompanyTask : BaseAuditableEntity
{
    public int Id { get; set; }

    public int CompanyId { get; set; }

    public string? Name { get; set; }

    public bool Active { get; set; }

    public Company? Company { get; set; }
}
