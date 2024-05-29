using EmployeeControl.Application.Common.Extensions;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Constants;
using EmployeeControl.Domain.Entities;
using EmployeeControl.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace EmployeeControl.Application.Features.Employees.Commands.AddRoleHumanResources;

internal class AddRoleHumanResourcesHandler(
    UserManager<User> userManager,
    IUserRepository userRepository,
    ILogger<AddRoleHumanResourcesHandler> logger)
    : ICommandHandler<AddRoleHumanResourcesCommand>
{
    public async Task<Result> Handle(AddRoleHumanResourcesCommand request, CancellationToken cancellationToken)
    {
        var employee = await userRepository.GetByIdAsync(request.EmployeeId);
        var identityResult = await userManager.AddToRoleAsync(employee, Roles.HumanResources);

        if (identityResult.Succeeded)
        {
            return Result.Success();
        }

        logger.LogError("{Errors}", identityResult.Errors);

        return identityResult.ToResult();
    }
}
