using EmployeeControl.Application.Common.Interfaces.Features.Identity;
using EmployeeControl.Application.Common.Interfaces.Features.TimesControl;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;

namespace EmployeeControl.Application.Features.TimesControl.Commands.StartTimeControl;

internal class StartTimeControlHandler(ITimesControlService timesControlService, IIdentityService identityService)
    : ICommandHandler<StartTimeControlCommand, string>
{
    public async Task<Result<string>> Handle(StartTimeControlCommand request, CancellationToken cancellationToken)
    {
        var employee = await identityService.GetByIdAsync(request.EmployeeId);

        var timeControl = await timesControlService.StartAsync(
            employee,
            request.DeviceType,
            request.Latitude,
            request.Longitude,
            cancellationToken);

        return Result.Success(timeControl.Id);
    }
}
