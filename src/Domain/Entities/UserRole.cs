using Microsoft.AspNetCore.Identity;

namespace EmployeeControl.Domain.Entities;

public class UserRole : IdentityRole<Guid>
{
    public UserRole(string name)
        : base(name)
    {
    }
}
