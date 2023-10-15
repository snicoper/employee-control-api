using EmployeeControl.Application.Common.Extensions;
using EmployeeControl.Application.Common.Interfaces.Identity;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Constants;
using EmployeeControl.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl.Application.Common.Services.Identity;

public class IdentityService(
        UserManager<ApplicationUser> userManager,
        IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
        IAuthorizationService authorizationService)
    : IIdentityService
{
    public async Task<string?> GetUserNameAsync(string userId)
    {
        var user = await userManager.Users.FirstAsync(u => u.Id == userId);

        return user.UserName;
    }

    public async Task<bool> IsInRoleAsync(string userId, string role)
    {
        var user = userManager.Users.SingleOrDefault(u => u.Id == userId);

        return user != null && await userManager.IsInRoleAsync(user, role);
    }

    public async Task<bool> AuthorizeAsync(string userId, string policyName)
    {
        var user = userManager.Users.SingleOrDefault(u => u.Id == userId);

        if (user is null)
        {
            return false;
        }

        var principal = await userClaimsPrincipalFactory.CreateAsync(user);

        var result = await authorizationService.AuthorizeAsync(principal, policyName);

        return result.Succeeded;
    }

    public async Task<(Result Result, string Id)> CreateUserAsync(ApplicationUser applicationUser, string password)
    {
        if (string.IsNullOrEmpty(applicationUser.UserName))
        {
            applicationUser.UserName = applicationUser.Email;
        }

        var result = await userManager.CreateAsync(applicationUser, password);
        await userManager.AddToRolesAsync(applicationUser, new[] { Roles.Employee });

        return (result.ToApplicationResult(), applicationUser.Id);
    }

    public async Task<Result> DeleteUserAsync(ApplicationUser applicationUser)
    {
        var result = await userManager.DeleteAsync(applicationUser);

        return result.ToApplicationResult();
    }
}
