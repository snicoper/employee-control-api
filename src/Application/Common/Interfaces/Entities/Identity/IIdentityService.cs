using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Common.Interfaces.Entities.Identity;

public interface IIdentityService
{
    Task<string?> GetUserNameAsync(string userId);

    Task<bool> IsInRoleAsync(string userId, string role);

    Task<bool> AuthorizeAsync(string userId, string policyName);

    IQueryable<ApplicationUser> GetAccountsByCompanyId(string companyId);

    Task<(Result Result, string Id)> CreateAccountAsync(
        ApplicationUser user,
        string password,
        IEnumerable<string> roles,
        CancellationToken cancellationToken);

    Task<Result> UpdateAccountAsync(ApplicationUser user, CancellationToken cancellationToken);

    Task<Result> DeleteAccountAsync(ApplicationUser user);
}
