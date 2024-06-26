﻿using EmployeeControl.Domain.Common;

namespace EmployeeControl.Domain.Entities;

public class WorkingDaysWeek : BaseAuditableEntity
{
    public bool Monday { get; set; }

    public bool Tuesday { get; set; }

    public bool Wednesday { get; set; }

    public bool Thursday { get; set; }

    public bool Friday { get; set; }

    public bool Saturday { get; set; }

    public bool Sunday { get; set; }

    public Guid CompanyId { get; set; }

    public Company Company { get; set; } = null!;
}
