using EmployeeControl.Application.Common.Constants;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Application.Common.Services.Hubs;
using EmployeeControl.Domain.Repositories;
using Microsoft.AspNetCore.SignalR;

namespace EmployeeControl.Application.Features.TimesControl.Commands.UpdateTimeControl;

internal class UpdateTimeControlHandler(
    ITimeControlRepository timeControlRepository,
    IHubContext<NotificationTimeControlIncidenceHub> hubContext)
    : ICommandHandler<UpdateTimeControlCommand>
{
    public async Task<Result> Handle(UpdateTimeControlCommand request, CancellationToken cancellationToken)
    {
        var timeControl = await timeControlRepository.GetByIdAsync(request.Id, cancellationToken);

        timeControl.Start = request.Start;
        timeControl.Finish = request.Finish;

        if (request.CloseIncidence)
        {
            timeControl.Incidence = false;
        }

        await timeControlRepository.UpdateAsync(timeControl, cancellationToken);

        if (request.CloseIncidence)
        {
            // Notificar SignalR de cierre de una incidencia.
            await hubContext.Clients.All.SendAsync(HubNames.TimeControlIncidences, cancellationToken);
        }

        return Result.Success();
    }
}
