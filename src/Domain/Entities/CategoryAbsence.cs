using EmployeeControl.Domain.Common;

namespace EmployeeControl.Domain.Entities;

public class CategoryAbsence : BaseAuditableEntity
{
    public string Description { get; set; } = default!;

    public string Background { get; set; } = default!;

    public string Color { get; set; } = default!;

    public bool Active { get; set; }

    public Guid CompanyId { get; set; }

    public Company Company { get; set; } = null!;
}
