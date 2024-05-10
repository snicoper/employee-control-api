using EmployeeControl.Application.Common.Interfaces.Features.Identity;
using EmployeeControl.Application.Common.Interfaces.Features.TimesControl;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Enums;

namespace EmployeeControl.Application.Features.TimesControl.Commands.FinishTimeControl;

internal class FinishTimeControlHandler(IIdentityService identityService, ITimesControlService timesControlService)
    : ICommandHandler<FinishTimeControlCommand>
{
    public async Task<Result> Handle(FinishTimeControlCommand request, CancellationToken cancellationToken)
    {
        var employee = await identityService.GetByIdAsync(request.EmployeeId);

        await timesControlService.FinishAsync(
            employee,
            request.DeviceType,
            ClosedBy.Employee,
            request.Latitude,
            request.Longitude,
            cancellationToken);

        return Result.Success();
    }
}
