using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Common.Interfaces.Users;

public interface ITokenService
{
    Task<string> GenerateAccessTokenAsync(User user);

    string GenerateRefreshToken();
}
