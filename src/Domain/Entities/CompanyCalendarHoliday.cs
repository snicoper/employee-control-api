﻿using EmployeeControl.Domain.Common;

namespace EmployeeControl.Domain.Entities;

public class CompanyCalendarHoliday : BaseAuditableEntity
{
    public DateOnly Date { get; set; }

    public string Description { get; set; } = default!;

    public Guid CompanyCalendarId { get; set; } = default!;

    public CompanyCalendar CompanyCalendar { get; set; } = default!;
}
