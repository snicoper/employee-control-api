using System.Security.Claims;
using EmployeeControl.Application.Common.Constants;
using EmployeeControl.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace EmployeeControl.Application.Common.Services;

public class CustomClaimsPrincipalFactory(UserManager<User> userManager, IOptions<IdentityOptions> optionsAccessor)
    : UserClaimsPrincipalFactory<User>(userManager, optionsAccessor)
{
    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
    {
        var identity = await base.GenerateClaimsAsync(user);

        identity.AddClaim(new Claim(CustomClaims.CompanyId, user.CompanyId.ToString()));

        return identity;
    }
}
