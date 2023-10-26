using EmployeeControl.Application.Common.Interfaces.Entities.Identity;
using EmployeeControl.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace EmployeeControl.Infrastructure.Services.Entities.Identity;

public class IdentityRoleService(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
    : IIdentityRoleService
{
    public async Task<List<IdentityRole>> GetRolesByUseAsync(ApplicationUser user)
    {
        var identityRoles = new List<IdentityRole>();
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
