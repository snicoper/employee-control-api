using EmployeeControl.Application.Common.Interfaces;
using EmployeeControl.Application.Common.Interfaces.Common;
using System.Security.Claims;

namespace EmployeeControl.WebApi.Services;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor)
    : ICurrentUserService
{
    public string? Id => httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Sid);
}
