﻿using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Repositories;

namespace EmployeeControl.Application.Features.TimesControl.Commands.StartTimeControl;

internal class StartTimeControlHandler(ITimeControlRepository timeControlRepository, IUserRepository userRepository)
    : ICommandHandler<StartTimeControlCommand, string>
{
    public async Task<Result<string>> Handle(StartTimeControlCommand request, CancellationToken cancellationToken)
    {
        var employee = await userRepository.GetByIdAsync(request.EmployeeId);

        var timeControl = await timeControlRepository.StartAsync(
            employee,
            request.DeviceType,
            request.Latitude,
            request.Longitude,
            cancellationToken);

        return Result.Success(timeControl.Id);
    }
}
