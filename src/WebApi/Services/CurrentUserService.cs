using System.Security.Claims;
using EmployeeControl.Application.Common.Interfaces.Common;

namespace EmployeeControl.WebApi.Services;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor)
    : ICurrentUserService
{
    public string? Id => httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Sid);
}
