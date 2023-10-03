﻿namespace EmployeeControl.Application.Common.Models;

public abstract class BaseAuditableDto : BaseDto
{
    public DateTimeOffset Created { get; set; }

    public string? CreatedBy { get; set; }

    public DateTimeOffset LastModified { get; set; }

    public string? LastModifiedBy { get; set; }
}
