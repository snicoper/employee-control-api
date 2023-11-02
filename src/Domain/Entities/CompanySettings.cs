using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Interfaces;

namespace EmployeeControl.Domain.Entities;

public class CompanySettings : BaseAuditableEntity, ICompany
{
    public string? Timezone { get; set; }

    public string CompanyId { get; set; } = default!;

    public Company? Company { get; set; }
}
