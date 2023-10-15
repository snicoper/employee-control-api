using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Common.Interfaces.Identity;

public interface IIdentityService
{
    Task<string?> GetUserNameAsync(string userId);

    Task<bool> IsInRoleAsync(string userId, string role);

    Task<bool> AuthorizeAsync(string userId, string policyName);

    Task<(Result Result, string Id)> CreateUserAsync(ApplicationUser applicationUser, string password, IEnumerable<string> roles);

    Task<Result> DeleteUserAsync(ApplicationUser applicationUser);
}
