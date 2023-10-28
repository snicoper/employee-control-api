﻿using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Interfaces;

namespace EmployeeControl.Domain.Entities;

public class CompanyTask : BaseAuditableEntity, ICompanyId
{
    public string? Name { get; set; }

    public bool Active { get; set; }

    public Company? Company { get; set; }

    public string CompanyId { get; set; } = default!;
}
