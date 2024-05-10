using EmployeeControl.Application.Common.Extensions;
using EmployeeControl.Application.Common.Interfaces.Features.Identity;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Application.Features.Employees.Commands.AddRoleHumanResources;
using EmployeeControl.Domain.Constants;
using EmployeeControl.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace EmployeeControl.Application.Features.Employees.Commands.RemoveRoleHumanResources;

internal class RemoveRoleHumanResourcesHandler(
    UserManager<User> userManager,
    IIdentityService identityService,
    ILogger<AddRoleHumanResourcesHandler> logger)
    : ICommandHandler<RemoveRoleHumanResourcesCommand>
{
    public async Task<Result> Handle(RemoveRoleHumanResourcesCommand request, CancellationToken cancellationToken)
    {
        var employee = await identityService.GetByIdAsync(request.EmployeeId);
        var identityResult = await userManager.RemoveFromRoleAsync(employee, Roles.HumanResources);

        if (identityResult.Succeeded)
        {
            return Result.Success();
        }

        logger.LogError("{Errors}", identityResult.Errors);

        return identityResult.ToApplicationResult();
    }
}
