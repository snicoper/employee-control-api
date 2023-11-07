﻿using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Enums;
using EmployeeControl.Domain.Interfaces;

namespace EmployeeControl.Domain.Entities;

public class TimeControl : BaseAuditableEntity, ICompany
{
    public string UserId { get; set; } = default!;

    public DateTimeOffset Start { get; set; }

    public DateTimeOffset Finish { get; set; }

    public ClosedBy ClosedBy { get; set; } = ClosedBy.Unclosed;

    public TimeState TimeState { get; set; } = TimeState.Open;

    public ApplicationUser User { get; set; } = null!;

    public string CompanyId { get; set; } = default!;

    public Company Company { get; set; } = null!;
}