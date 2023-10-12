using AutoMapper;
using EmployeeControl.Application.Common.Extensions;
using EmployeeControl.Application.Common.Interfaces.Identity;
using EmployeeControl.Domain.Constants;
using EmployeeControl.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EmployeeControl.Application.Features.Identity.Commands.IdentityRegister;

internal class IdentityRegisterHandler(IIdentityService identityService, RoleManager<IdentityRole> roleManager, IMapper mapper)
    : IRequestHandler<IdentityRegisterCommand, string>
{
    public async Task<string> Handle(IdentityRegisterCommand request, CancellationToken cancellationToken)
    {
        var user = mapper.Map<IdentityRegisterCommand, ApplicationUser>(request);
        var password = request.Password.SetEmptyIfNull();
        var resultResponse = await identityService.CreateUserAsync(user, password);

        // Default roles for user.
        var createRole = new List<IdentityRole>
        {
            new(Roles.EnterpriseAdministrator), new(Roles.HumanResources), new(Roles.Employee)
        };

        foreach (var identityRole in createRole.Where(identityRole => roleManager.Roles.All(r => r.Name != identityRole.Name)))
        {
            await roleManager.CreateAsync(identityRole);
        }

        return resultResponse.Id;
    }
}
