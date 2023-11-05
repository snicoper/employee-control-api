﻿using EmployeeControl.Domain.Interfaces;

namespace EmployeeControl.Domain.Entities;

public class UserDepartment : ICompany
{
    public string DepartmentId { get; set; } = default!;

    public Department Department { get; set; } = null!;

    public string UserId { get; set; } = default!;

    public ApplicationUser User { get; set; } = null!;

    public string CompanyId { get; set; } = default!;

    public Company Company { get; set; } = null!;
}
