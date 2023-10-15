using EmployeeControl.Application.Common.Constants;
using EmployeeControl.Application.Common.Extensions;
using EmployeeControl.Application.Common.Interfaces;
using EmployeeControl.Application.Common.Interfaces.Identity;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl.Infrastructure.Services.Identity;

public class IdentityService(
        UserManager<ApplicationUser> userManager,
        IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
        IAuthorizationService authorizationService,
        IValidateCreateIdentityService validateCreateIdentityService,
        IValidationFailureService validationFailureService)
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

    public async Task<Result> DeleteUserAsync(ApplicationUser applicationUser)
    {
        var result = await userManager.DeleteAsync(applicationUser);

        return result.ToApplicationResult();
    }

    public async Task<(Result Result, string Id)> CreateUserAsync(
        ApplicationUser applicationUser,
        string password,
        IEnumerable<string> roles)
    {
        await validateCreateIdentityService.UserValidationAsync(applicationUser);
        await validateCreateIdentityService.PasswordValidationAsync(applicationUser, password);

        applicationUser.Active = true;

        if (string.IsNullOrEmpty(applicationUser.UserName))
        {
            applicationUser.UserName = applicationUser.Email;
        }

        var result = await userManager.CreateAsync(applicationUser, password);

        if (!result.Succeeded)
        {
            foreach (var identityError in result.Errors)
            {
                validationFailureService.Add(ValidationErrorsKeys.Identity, identityError.Description);
            }

            validationFailureService.RaiseException();
        }

        await userManager.AddToRolesAsync(applicationUser, roles);

        return (result.ToApplicationResult(), applicationUser.Id);
    }
}
