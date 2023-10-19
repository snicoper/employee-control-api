using EmployeeControl.Application.Common.Extensions;
using EmployeeControl.Application.Common.Interfaces;
using EmployeeControl.Application.Common.Interfaces.Common;
using EmployeeControl.Application.Common.Interfaces.Entities.Identity;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace EmployeeControl.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest>(
        ILogger<TRequest> logger,
        ICurrentUserService currentUserService,
        IIdentityService identityService)
    : IRequestPreProcessor<TRequest>
    where TRequest : notnull
{
    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var userId = currentUserService.Id.SetEmptyIfNull();
        var userName = string.Empty;

        if (!string.IsNullOrEmpty(userId))
        {
            userName = await identityService.GetUserNameAsync(userId);
        }

        logger.LogInformation(
            "CleanArchitecture Request: {Name} {@UserId} {@UserName} {@Request}",
            requestName,
            userId,
            userName,
            request);
    }
}
