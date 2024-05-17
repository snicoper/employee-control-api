using EmployeeControl.Application.Common.Interfaces.Features.TimesControl;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Enums;
using EmployeeControl.Domain.Repositories;

namespace EmployeeControl.Application.Features.TimesControl.Commands.FinishTimeControlByStaff;

internal class FinishTimeControlByStaffHandler(IUserRepository userRepository, ITimesControlService timesControlService)
    : ICommandHandler<FinishTimeControlByStaffCommand>
{
    public async Task<Result> Handle(FinishTimeControlByStaffCommand request, CancellationToken cancellationToken)
    {
        var timeControl = await timesControlService.GetByIdAsync(request.TimeControlId, cancellationToken);
        var employee = await userRepository.GetByIdAsync(timeControl.UserId);

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
