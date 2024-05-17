using EmployeeControl.Application.Common.Interfaces.Features.TimesControl;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Enums;
using EmployeeControl.Domain.Repositories;

namespace EmployeeControl.Application.Features.TimesControl.Commands.FinishTimeControl;

internal class FinishTimeControlHandler(IUserRepository userRepository, ITimesControlService timesControlService)
    : ICommandHandler<FinishTimeControlCommand>
{
    public async Task<Result> Handle(FinishTimeControlCommand request, CancellationToken cancellationToken)
    {
        var employee = await userRepository.GetByIdAsync(request.EmployeeId);

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
