using EmployeeControl.Application.Common.Extensions;
using EmployeeControl.Application.Common.Interfaces.Common;
using EmployeeControl.Application.Common.Interfaces.Entities.Identity;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl.Infrastructure.Services.Entities.Identity;

public class IdentityService(
        UserManager<ApplicationUser> userManager,
        IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
        IAuthorizationService authorizationService,
        IIdentityValidatorService identityValidatorService,
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
        IEnumerable<string> roles,
        CancellationToken cancellationToken)
    {
        applicationUser.Active = true;
        applicationUser.UserName = applicationUser.Email;

        // Validaciones.
        await identityValidatorService.UserValidationAsync(applicationUser);
        await identityValidatorService.PasswordValidationAsync(applicationUser, password);
        await identityValidatorService.UniqueEmailValidationAsync(applicationUser, cancellationToken);
        validationFailureService.RaiseExceptionIfExistsErrors();

        var identityResult = await userManager.CreateAsync(applicationUser, password);

        await userManager.AddToRolesAsync(applicationUser, roles);

        return (identityResult.ToApplicationResult(), applicationUser.Id);
    }
}
