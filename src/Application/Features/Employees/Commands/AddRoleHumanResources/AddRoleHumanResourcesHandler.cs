using EmployeeControl.Application.Common.Exceptions;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Constants;
using EmployeeControl.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace EmployeeControl.Application.Features.Employees.Commands.AddRoleHumanResources;

internal class AddRoleHumanResourcesHandler(
        UserManager<ApplicationUser> userManager,
        ILogger<AddRoleHumanResourcesHandler> logger)
    : IRequestHandler<AddRoleHumanResourcesCommand, Result>
{
    public async Task<Result> Handle(AddRoleHumanResourcesCommand request, CancellationToken cancellationToken)
    {
        var employee = await userManager.FindByIdAsync(request.EmployeeId)
                       ?? throw new NotFoundException(nameof(ApplicationUser), nameof(ApplicationUser.Id));

        var identityResult = await userManager.AddToRoleAsync(employee, Roles.HumanResources);

        if (identityResult.Succeeded)
        {
            return Result.Success();
        }

        logger.LogDebug("{error}", identityResult.Errors);

        return Result.Failure(string.Empty);
    }
}
