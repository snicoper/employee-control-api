using EmployeeControl.Application.Common.Exceptions;
using EmployeeControl.Application.Common.Extensions;
using EmployeeControl.Application.Common.Interfaces.Common;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Features.Identity;
using EmployeeControl.Application.Common.Interfaces.Users;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Constants;
using EmployeeControl.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmployeeControl.Infrastructure.Services.Features.Identity;

public class IdentityService(
    UserManager<User> userManager,
    IApplicationDbContext context,
    IUserClaimsPrincipalFactory<User> userClaimsPrincipalFactory,
    IAuthorizationService authorizationService,
    IIdentityValidatorService identityValidatorService,
    ICurrentUserService currentUserService,
    IDateTimeService dateTimeService,
    ILogger<IdentityService> logger)
    : IIdentityService
{
    public async Task<string?> GetUserNameAsync(string userId)
    {
        var user = await userManager
            .Users
            .FirstAsync(u => u.Id == userId);

        return user.UserName;
    }

    public async Task<User> GetByIdAsync(string userId)
    {
        return await userManager.FindByIdAsync(userId) ??
               throw new NotFoundException(nameof(User), nameof(User.Id));
    }

    public async Task<User> GetByIdWithCompanyCalendarAsync(string userId)
    {
        var user = await userManager
                       .Users
                       .Include(u => u.CompanyCalendar)
                       .SingleOrDefaultAsync(u => u.Id == userId) ??
                   throw new NotFoundException(nameof(User), nameof(User.Id));

        return user;
    }

    public async Task<User> GetCurrentAsync()
    {
        var user = await GetByIdAsync(currentUserService.Id);

        return user;
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        return await userManager.FindByEmailAsync(email) ??
               throw new NotFoundException(nameof(User), nameof(User.Email));
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

    public IQueryable<User> GetAllQueryable()
    {
        var users = userManager.Users;

        return users;
    }

    public async Task<(Result Result, string Id)> CreateAsync(
        User user,
        string password,
        IEnumerable<string> roles,
        CancellationToken cancellationToken)
    {
        user.Active = true;
        user.UserName = user.Email;
        user.EntryDate = dateTimeService.UtcNow;

        // Validaciones.
        var result = Result.Create();
        await identityValidatorService.UserValidationAsync(user, result);
        await identityValidatorService.PasswordValidationAsync(user, password, result);
        await identityValidatorService.UniqueEmailValidationAsync(user, result, cancellationToken);
        result.RaiseBadRequestIfResultFailure();

        var identityResult = await userManager.CreateAsync(user, password);

        await userManager.AddToRolesAsync(user, roles);

        return (identityResult.ToApplicationResult(), user.Id);
    }

    public async Task<Result> UpdateAsync(User user, CancellationToken cancellationToken)
    {
        // Validaciones.
        var result = Result.Create();
        await identityValidatorService.UserValidationAsync(user, result);
        await identityValidatorService.UniqueEmailValidationAsync(user, result, cancellationToken);
        result.RaiseBadRequestIfResultFailure();

        user.UserName = user.Email;

        await userManager.UpdateNormalizedEmailAsync(user);
        await userManager.UpdateNormalizedUserNameAsync(user);
        var identityResult = await userManager.UpdateAsync(user);

        return identityResult.ToApplicationResult();
    }

    public async Task<Result> DeleteAsync(User user)
    {
        var result = await userManager.DeleteAsync(user);

        return result.ToApplicationResult();
    }

    public async Task<Result> UpdateRolesByUserIdAsync(
        User user,
        List<string> rolesToAdd,
        CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            // Obtener todos los roles y eliminarlos del usuario.
            var userRoles = await userManager.GetRolesAsync(user);

            var result = Result.Create();
            identityValidatorService.ValidateUpdateEmployeeRoles(user, userRoles, result);
            result.RaiseBadRequestIfResultFailure();

            // El rol de Employee es requerido.
            if (!rolesToAdd.Exists(r => r.Equals(Roles.Employee)))
            {
                rolesToAdd.Add(Roles.Employee);
            }

            var removeRolesResult = await userManager.RemoveFromRolesAsync(user, userRoles);

            if (!removeRolesResult.Succeeded)
            {
                await transaction.RollbackAsync(cancellationToken);

                return removeRolesResult.ToApplicationResult();
            }

            // Añade los nuevos roles al usuario.
            var addRolesResult = await userManager.AddToRolesAsync(user, rolesToAdd);

            if (!addRolesResult.Succeeded)
            {
                await transaction.RollbackAsync(cancellationToken);

                return addRolesResult.ToApplicationResult();
            }

            await transaction.CommitAsync(cancellationToken);

            return Result.Success();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error al actualizar roles al usuario.");
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
