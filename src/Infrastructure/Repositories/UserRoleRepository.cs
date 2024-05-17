using EmployeeControl.Domain.Entities;
using EmployeeControl.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace EmployeeControl.Infrastructure.Repositories;

public class UserRoleRepository(RoleManager<ApplicationRole> roleManager, UserManager<User> userManager)
    : IUserRoleRepository
{
    public async Task<List<ApplicationRole>> GetRolesByUseAsync(User user)
    {
        var identityRoles = new List<ApplicationRole>();
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
