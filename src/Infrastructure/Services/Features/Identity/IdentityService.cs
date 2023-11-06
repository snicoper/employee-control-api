using EmployeeControl.Application.Common.Exceptions;
using EmployeeControl.Application.Common.Extensions;
using EmployeeControl.Application.Common.Interfaces.Common;
using EmployeeControl.Application.Common.Interfaces.Features.Identity;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Constants;
using EmployeeControl.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl.Infrastructure.Services.Features.Identity;

public class IdentityService(
    UserManager<ApplicationUser> userManager,
    IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
    IAuthorizationService authorizationService,
    IIdentityValidatorService identityValidatorService,
    IValidationFailureService validationFailureService,
    ICurrentUserService currentUserService)
    : IIdentityService
{
    public async Task<string?> GetUserNameAsync(string userId)
    {
        var user = await userManager
            .Users
            .FirstAsync(u => u.Id == userId);

        return user.UserName;
    }

    public async Task<ApplicationUser> GetByIdAsync(string userId)
    {
        return await userManager.FindByIdAsync(userId) ??
               throw new NotFoundException(nameof(ApplicationUser), nameof(ApplicationUser.Email));
    }

    public async Task<ApplicationUser> GetByEmailAsync(string email)
    {
        return await userManager.FindByEmailAsync(email) ??
               throw new NotFoundException(nameof(ApplicationUser), nameof(ApplicationUser.Email));
    }

    public async Task<bool> IsInRoleAsync(string userId, string role)
    {
        var user = userManager
            .Users
            .SingleOrDefault(u => u.Id == userId);

        var result = user != null && await userManager.IsInRoleAsync(user, role);

        return result;
    }

    public async Task<bool> AuthorizeAsync(string userId, string policyName)
    {
        var user = userManager
            .Users
            .SingleOrDefault(u => u.Id == userId);

        if (user is null)
        {
            return false;
        }

        var principal = await userClaimsPrincipalFactory.CreateAsync(user);

        var result = await authorizationService.AuthorizeAsync(principal, policyName);

        return result.Succeeded;
    }

    public bool ItsFromTheCompany(string companyId)
    {
        // Roles de administración, se excluyen.
        if (currentUserService.Roles.Contains(Roles.SiteStaff))
        {
            return true;
        }

        var result = currentUserService.CompanyId.Equals(companyId, StringComparison.OrdinalIgnoreCase);

        return result;
    }

    public IQueryable<ApplicationUser> GetByCompanyId(string companyId)
    {
        var users = userManager
            .Users
            .Where(au => au.CompanyId == companyId);

        return users;
    }

    public async Task<(Result Result, string Id)> CreateAsync(
        ApplicationUser user,
        string password,
        IEnumerable<string> roles,
        CancellationToken cancellationToken)
    {
        user.Active = true;
        user.UserName = user.Email;

        // Validaciones.
        await identityValidatorService.UserValidationAsync(user);
        await identityValidatorService.PasswordValidationAsync(user, password);
        await identityValidatorService.UniqueEmailValidationAsync(user, cancellationToken);
        validationFailureService.RaiseExceptionIfExistsErrors();

        var identityResult = await userManager.CreateAsync(user, password);

        await userManager.AddToRolesAsync(user, roles);

        return (identityResult.ToApplicationResult(), user.Id);
    }

    public async Task<Result> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        // Validaciones.
        await identityValidatorService.UserValidationAsync(user);
        await identityValidatorService.UniqueEmailValidationAsync(user, cancellationToken);
        validationFailureService.RaiseExceptionIfExistsErrors();

        user.UserName = user.Email;

        await userManager.UpdateNormalizedEmailAsync(user);
        await userManager.UpdateNormalizedUserNameAsync(user);
        var identityResult = await userManager.UpdateAsync(user);

        return identityResult.ToApplicationResult();
    }

    public async Task<Result> DeleteAsync(ApplicationUser user)
    {
        var result = await userManager.DeleteAsync(user);

        return result.ToApplicationResult();
    }
}
