using EmployeeControl.Domain.Common;

namespace EmployeeControl.Domain.Entities;

/// <summary>
/// Días festivos de la empresa.
/// </summary>
public class CompanyCalendarHoliday : BaseAuditableEntity
{
    public DateOnly Date { get; set; }

    public string Description { get; set; } = default!;

    public string CompanyHolidayGroupId { get; set; } = default!;

    public CompanyCalendar CompanyCalendar { get; set; } = default!;
}
