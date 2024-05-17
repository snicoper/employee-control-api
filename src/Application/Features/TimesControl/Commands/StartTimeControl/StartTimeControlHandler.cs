using EmployeeControl.Application.Common.Interfaces.Features.TimesControl;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Repositories;

namespace EmployeeControl.Application.Features.TimesControl.Commands.StartTimeControl;

internal class StartTimeControlHandler(ITimesControlService timesControlService, IUserRepository userRepository)
    : ICommandHandler<StartTimeControlCommand, string>
{
    public async Task<Result<string>> Handle(StartTimeControlCommand request, CancellationToken cancellationToken)
    {
        var employee = await userRepository.GetByIdAsync(request.EmployeeId);

        var timeControl = await timesControlService.StartAsync(
            employee,
            request.DeviceType,
            request.Latitude,
            request.Longitude,
            cancellationToken);

        return Result.Success(timeControl.Id);
    }
}
