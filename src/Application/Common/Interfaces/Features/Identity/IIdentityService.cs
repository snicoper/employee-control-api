﻿using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Common.Interfaces.Features.Identity;

public interface IIdentityService
{
    Task<string?> GetUserNameAsync(string userId);

    Task<ApplicationUser> GetByIdAsync(string userId);

    Task<ApplicationUser> GetByEmailAsync(string email);

    Task<bool> IsInRoleAsync(string userId, string role);

    Task<bool> AuthorizeAsync(string userId, string policyName);

    bool ItsFromTheCompany(string companyId);

    IQueryable<ApplicationUser> GetByCompanyId(string companyId);

    Task<(Result Result, string Id)> CreateAsync(
        ApplicationUser user,
        string password,
        IEnumerable<string> roles,
        CancellationToken cancellationToken);

    Task<Result> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken);

    Task<Result> DeleteAsync(ApplicationUser user);
}
