using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Common.Interfaces.Identity;

public interface ITokenService
{
    Task<string> GenerateAccessTokenAsync(ApplicationUser user);

    string GenerateRefreshToken();
}
