﻿using EmployeeControl.Application.Common.Extensions;
using EmployeeControl.Application.Common.Interfaces.Common;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Users;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Constants;
using EmployeeControl.Domain.Entities;
using EmployeeControl.Domain.Exceptions;
using EmployeeControl.Domain.Repositories;
using EmployeeControl.Domain.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmployeeControl.Infrastructure.Repositories;

public class UserRepository(
    IApplicationDbContext context,
    UserManager<User> userManager,
    IUserClaimsPrincipalFactory<User> userClaimsPrincipalFactory,
    IAuthorizationService authorizationService,
    ICurrentUserService currentUserService,
    IIdentityValidator identityValidator,
    IDateTimeProvider dateTimeProvider,
    ILogger<UserRepository> logger)
    : IUserRepository
{
    public async Task<string?> GetUserNameAsync(Guid userId)
    {
        var user = await userManager
            .Users
            .FirstAsync(u => u.Id == userId);

        return user.UserName;
    }

    public async Task<User> GetByIdAsync(Guid userId)
    {
        return await userManager.FindByIdAsync(userId.ToString())
            ?? throw new NotFoundException(nameof(User), nameof(User.Id));
    }

    public async Task<User> GetByIdWithCompanyCalendarAsync(Guid userId)
    {
        var user = await userManager
                .Users
                .Include(u => u.CompanyCalendar)
                .SingleOrDefaultAsync(u => u.Id == userId)
            ?? throw new NotFoundException(nameof(User), nameof(User.Id));

        return user;
    }

    public async Task<User> GetCurrentAsync()
    {
        var user = await GetByIdAsync(currentUserService.Id);

        return user;
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        return await userManager
                .FindByEmailAsync(email)
            ?? throw new NotFoundException(nameof(User), nameof(User.Email));
    }

    public async Task<bool> IsInRoleAsync(Guid userId, string role)
    {
        var user = userManager
            .Users
            .SingleOrDefault(u => u.Id == userId);

        var result = user != null && await userManager.IsInRoleAsync(user, role);

        return result;
    }

    public async Task<bool> AuthorizeAsync(Guid userId, string policyName)
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

    public async Task<(IdentityResult IdentityResult, User User)> CreateAsync(
        User user,
        string password,
        IEnumerable<string> roles,
        CancellationToken cancellationToken)
    {
        user.Active = true;
        user.UserName = user.Email;
        user.EntryDate = dateTimeProvider.UtcNow;

        // Validaciones.
        var result = Result.Create();
        await identityValidator.UserValidationAsync(user, result);
        await identityValidator.PasswordValidationAsync(user, password, result);
        await identityValidator.UniqueEmailValidationAsync(user, result, cancellationToken);
        result.RaiseBadRequestIfErrorsExist();

        var identityResult = await userManager.CreateAsync(user, password);

        await userManager.AddToRolesAsync(user, roles);

        return (identityResult, user);
    }

    public async Task<(IdentityResult IdentityResult, User User)> UpdateAsync(User user, CancellationToken cancellationToken)
    {
        // Validaciones.
        var result = Result.Create();
        await identityValidator.UserValidationAsync(user, result);
        await identityValidator.UniqueEmailValidationAsync(user, result, cancellationToken);
        result.RaiseBadRequestIfErrorsExist();

        user.UserName = user.Email;

        await userManager.UpdateNormalizedEmailAsync(user);
        await userManager.UpdateNormalizedUserNameAsync(user);
        var identityResult = await userManager.UpdateAsync(user);

        return (identityResult, user);
    }

    public async Task<IdentityResult> DeleteAsync(User user)
    {
        var identityResult = await userManager.DeleteAsync(user);

        return identityResult;
    }

    public async Task<IdentityResult> UpdateRolesByUserIdAsync(
        User user,
        List<string> rolesToAdd,
        CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var removeRolesResult = await RemoveRolesByUserIdAsync(user, rolesToAdd);

            if (!removeRolesResult.Succeeded)
            {
                await transaction.RollbackAsync(cancellationToken);

                return removeRolesResult;
            }

            var addRolesResult = await AddRolesByUserIdAsync(user, rolesToAdd);

            if (!addRolesResult.Succeeded)
            {
                await transaction.RollbackAsync(cancellationToken);

                return addRolesResult;
            }

            await transaction.CommitAsync(cancellationToken);

            return addRolesResult;
        }
        catch (OperationCanceledException ex)
        {
            logger.LogError(ex, "Error al actualizar roles al usuario.");
            await transaction.RollbackAsync(cancellationToken);

            throw new OperationCanceledException(ex.Message);
        }
    }

    private async Task<IdentityResult> RemoveRolesByUserIdAsync(User user, List<string> rolesToAdd)
    {
        var userRoles = await userManager.GetRolesAsync(user);

        var result = Result.Create();
        identityValidator.ValidateUpdateEmployeeRoles(user, userRoles, result);
        result.RaiseBadRequestIfErrorsExist();

        // El rol de Employee es requerido.
        if (!rolesToAdd.Exists(r => r.Equals(Roles.Employee)))
        {
            rolesToAdd.Add(Roles.Employee);
        }

        var identityResult = await userManager.RemoveFromRolesAsync(user, userRoles);

        return identityResult;
    }

    private async Task<IdentityResult> AddRolesByUserIdAsync(User user, List<string> rolesToAdd)
    {
        var identityResult = await userManager.AddToRolesAsync(user, rolesToAdd);

        return identityResult;
    }
}
