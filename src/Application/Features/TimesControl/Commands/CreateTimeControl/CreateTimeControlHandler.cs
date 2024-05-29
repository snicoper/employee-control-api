using AutoMapper;
using EmployeeControl.Application.Common.Constants;
using EmployeeControl.Application.Common.Extensions;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Localization;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Entities;
using EmployeeControl.Domain.Enums;
using EmployeeControl.Domain.Repositories;
using Microsoft.Extensions.Localization;

namespace EmployeeControl.Application.Features.TimesControl.Commands.CreateTimeControl;

internal class CreateTimeControlHandler(
    IMapper mapper,
    ITimeControlRepository timeControlRepository,
    IStringLocalizer<TimeControlResource> localizer)
    : ICommandHandler<CreateTimeControlCommand, string>
{
    public async Task<Result<string>> Handle(CreateTimeControlCommand request, CancellationToken cancellationToken)
    {
        // Si el empleado tiene algún tiempo iniciado, el nuevo deberá ser TimeState.close.
        var timeControlOpen = await timeControlRepository.GetTimeStateOpenByEmployeeIdAsync(request.UserId, cancellationToken);

        if (timeControlOpen is not null && request.TimeState == TimeState.Open)
        {
            var errorMessage =
                localizer["El empleado ya tiene un tiempo abierto y no es posible abrir otro, debe cerrar el tiempo."];
            Result.Failure(ValidationErrorsKeys.NotificationErrors, errorMessage).RaiseBadRequest();
        }

        TimeControl resultResponse;
        var timeControl = mapper.Map<TimeControl>(request);

        timeControl.Incidence = false;
        timeControl.DeviceTypeStart = request.DeviceType;

        if (request.TimeState == TimeState.Close)
        {
            timeControl.ClosedBy = ClosedBy.Staff;
            timeControl.TimeState = TimeState.Close;
            timeControl.DeviceTypeFinish = request.DeviceType;

            resultResponse = await timeControlRepository.CreateWithFinishAsync(timeControl, cancellationToken);
        }
        else
        {
            timeControl.Finish = timeControl.Start;
            timeControl.TimeState = TimeState.Open;

            resultResponse = await timeControlRepository.CreateWithOutFinishAsync(timeControl, cancellationToken);
        }

        return Result.Success(resultResponse.Id);
    }
}
