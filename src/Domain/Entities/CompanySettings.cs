using EmployeeControl.Domain.Common;

namespace EmployeeControl.Domain.Entities;

public class CompanySettings : BaseAuditableEntity
{
    public string Timezone { get; set; } = default!;

    public int PeriodTimeControlMax { get; set; } = 24;

    public int WeeklyWorkingHours { get; set; } = 40;

    public bool GeolocationRequired { get; set; }

    public Guid CompanyId { get; set; }

    public Company Company { get; set; } = null!;
}
