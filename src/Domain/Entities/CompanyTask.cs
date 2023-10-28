using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Interfaces;

namespace EmployeeControl.Domain.Entities;

public class CompanyTask : BaseAuditableEntity, ICompanyId
{
    public string? Name { get; set; }

    public bool Active { get; set; }

    public string? Background { get; set; }

    public string? Color { get; set; }

    public Company? Company { get; set; }

    public ICollection<UserCompanyTask> UserCompanyTasks { get; set; } = new List<UserCompanyTask>();

    public string CompanyId { get; set; } = default!;
}
