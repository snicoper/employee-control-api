using System.Security.Claims;
using EmployeeControl.Application.Common.Constants;
using EmployeeControl.Application.Common.Interfaces.Users;

namespace EmployeeControl.WebApi.Services.Users;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor)
    : ICurrentUserService
{
    public Guid Id
    {
        get
        {
            var id = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Sid);

            return id is null ? Guid.Empty : Guid.Parse(id);
        }
    }

    public Guid CompanyId
    {
        get
        {
            var companyId = httpContextAccessor.HttpContext?.User.FindFirstValue(CustomClaims.CompanyId);

            return companyId is null ? Guid.Empty : Guid.Parse(companyId);
        }
    }

    public IEnumerable<string> Roles =>
        httpContextAccessor.HttpContext?.User.FindAll(ClaimTypes.Role).Select(r => r.Value) ?? [];
}
