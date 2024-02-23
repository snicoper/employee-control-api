using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Interfaces;

namespace EmployeeControl.Domain.Entities;

public class CategoryAbsence : BaseAuditableEntity, ICompany
{
    public string Description { get; set; } = default!;

    public string Background { get; set; } = default!;

    public string Color { get; set; } = default!;

    public string CompanyId { get; set; } = default!;

    public Company Company { get; set; } = null!;
}
