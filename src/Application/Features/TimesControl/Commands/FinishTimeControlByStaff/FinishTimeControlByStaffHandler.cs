using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Enums;
using EmployeeControl.Domain.Repositories;

namespace EmployeeControl.Application.Features.TimesControl.Commands.FinishTimeControlByStaff;

internal class FinishTimeControlByStaffHandler(IUserRepository userRepository, ITimeControlRepository timeControlRepository)
    : ICommandHandler<FinishTimeControlByStaffCommand>
{
    public async Task<Result> Handle(FinishTimeControlByStaffCommand request, CancellationToken cancellationToken)
    {
        var timeControl = await timeControlRepository.GetByIdAsync(request.TimeControlId, cancellationToken);
        var employee = await userRepository.GetByIdAsync(timeControl.UserId);

        await timeControlRepository.FinishAsync(
            employee,
            DeviceType.System,
            ClosedBy.Staff,
            null,
            null,
            cancellationToken);

        return Result.Success();
    }
}
