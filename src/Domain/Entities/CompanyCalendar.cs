using EmployeeControl.Domain.Common;

namespace EmployeeControl.Domain.Entities;

/// <summary>
/// Grupo de calendario con días festivos.
/// </summary>
public class CompanyCalendar : BaseAuditableEntity
{
    public string Name { get; set; } = default!;

    public string Description { get; set; } = default!;

    public bool Default { get; set; }

    public ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();

    public ICollection<CompanyCalendarHoliday> CompanyCalendarHolidays { get; set; } = new List<CompanyCalendarHoliday>();
}
