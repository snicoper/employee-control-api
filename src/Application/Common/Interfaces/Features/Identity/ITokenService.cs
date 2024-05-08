using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Common.Interfaces.Features.Identity;

public interface ITokenService
{
    Task<string> GenerateAccessTokenAsync(User user);

    string GenerateRefreshToken();
}
