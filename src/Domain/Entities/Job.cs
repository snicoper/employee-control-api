using EmployeeControl.Domain.Common;

namespace EmployeeControl.Domain.Entities;

public class Job : BaseAuditableEntity
{
    public int Id { get; set; }

    public int CompanyId { get; set; }

    public string? Name { get; set; }

    public Company? Company { get; set; }
}
