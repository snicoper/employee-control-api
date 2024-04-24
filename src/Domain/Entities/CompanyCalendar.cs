using EmployeeControl.Domain.Common;

namespace EmployeeControl.Domain.Entities;

public class CompanyCalendar : BaseAuditableEntity
{
    public CompanyCalendar()
    {
        CompanyHolidays = new List<CompanyCalendarHoliday>();
        Users = new List<ApplicationUser>();
    }

    public string Name { get; set; } = default!;

    public string Description { get; set; } = default!;

    public ICollection<CompanyCalendarHoliday> CompanyHolidays { get; set; }

    public ICollection<ApplicationUser> Users { get; set; }
}
