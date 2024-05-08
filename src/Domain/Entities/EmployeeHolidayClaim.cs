﻿using EmployeeControl.Domain.Common;

namespace EmployeeControl.Domain.Entities;

/// <summary>
/// Cabecera de días de vacaciones.
/// </summary>
public class EmployeeHolidayClaim : BaseAuditableEntity
{
    public int Year { get; set; }

    public string? Description { get; set; }

    public bool Accepted { get; set; }

    public string UserId { get; set; } = default!;

    public User User { get; set; } = null!;

    public ICollection<EmployeeHolidayClaimItem> EmployeeHolidayClaimLines { get; set; } = new List<EmployeeHolidayClaimItem>();
}
