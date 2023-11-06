﻿using Microsoft.AspNetCore.Identity;

namespace EmployeeControl.Domain.Entities;

public class ApplicationRole : IdentityRole
{
    public ApplicationRole()
    {
    }

    public ApplicationRole(string roleName)
        : base(roleName)
    {
    }
}
