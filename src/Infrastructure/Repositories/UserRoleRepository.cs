using EmployeeControl.Domain.Entities;
using EmployeeControl.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace EmployeeControl.Infrastructure.Repositories;

public class UserRoleRepository(RoleManager<UserRole> roleManager, UserManager<User> userManager)
    : IUserRoleRepository
{
    public async Task<List<UserRole>> GetRolesByUseAsync(User user)
    {
        var identityRoles = new List<UserRole>();
        var userRoles = await userManager.GetRolesAsync(user);

        if (userRoles.Count == 0)
        {
            return identityRoles;
        }

        foreach (var role in roleManager.Roles.Where(r => r.Name != null && userRoles.Contains(r.Name)))
        {
            identityRoles.Add(role);
        }

        return identityRoles;
    }
}
