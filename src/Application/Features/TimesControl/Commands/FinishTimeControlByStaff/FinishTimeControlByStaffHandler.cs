using EmployeeControl.Application.Common.Interfaces.Features.Identity;
using EmployeeControl.Application.Common.Interfaces.Features.TimesControl;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Enums;

namespace EmployeeControl.Application.Features.TimesControl.Commands.FinishTimeControlByStaff;

internal class FinishTimeControlByStaffHandler(IIdentityService identityService, ITimesControlService timesControlService)
    : ICommandHandler<FinishTimeControlByStaffCommand>
{
    public async Task<Result> Handle(FinishTimeControlByStaffCommand request, CancellationToken cancellationToken)
    {
        var timeControl = await timesControlService.GetByIdAsync(request.TimeControlId, cancellationToken);
        var employee = await identityService.GetByIdAsync(timeControl.UserId);

        await timesControlService.FinishAsync(
            employee,
            DeviceType.System,
            ClosedBy.Staff,
            null,
            null,
            cancellationToken);

        return Result.Success();
    }
}
