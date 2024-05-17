using EmployeeControl.Application.Common.Extensions;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Application.Features.Employees.Commands.AddRoleHumanResources;
using EmployeeControl.Domain.Constants;
using EmployeeControl.Domain.Entities;
using EmployeeControl.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace EmployeeControl.Application.Features.Employees.Commands.RemoveRoleHumanResources;

internal class RemoveRoleHumanResourcesHandler(
    UserManager<User> userManager,
    IUserRepository userRepository,
    ILogger<AddRoleHumanResourcesHandler> logger)
    : ICommandHandler<RemoveRoleHumanResourcesCommand>
{
    public async Task<Result> Handle(RemoveRoleHumanResourcesCommand request, CancellationToken cancellationToken)
    {
        var employee = await userRepository.GetByIdAsync(request.EmployeeId);
        var identityResult = await userManager.RemoveFromRoleAsync(employee, Roles.HumanResources);

        if (identityResult.Succeeded)
        {
            return Result.Success();
        }

        logger.LogError("{Errors}", identityResult.Errors);

        return identityResult.ToApplicationResult();
    }
}
