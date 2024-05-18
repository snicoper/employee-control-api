using EmployeeControl.Application.Common.Constants;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Application.Common.Services.Hubs;
using EmployeeControl.Domain.Repositories;
using Microsoft.AspNetCore.SignalR;

namespace EmployeeControl.Application.Features.TimesControl.Commands.CreateIncidence;

internal class CreateIncidenceHandler(
    ITimeControlRepository timeControlRepository,
    IHubContext<NotificationTimeControlIncidenceHub> hubContext)
    : ICommandHandler<CreateIncidenceCommand>
{
    public async Task<Result> Handle(CreateIncidenceCommand request, CancellationToken cancellationToken)
    {
        var timeControl = await timeControlRepository.GetByIdAsync(request.TimeControlId, cancellationToken);
        timeControl.Incidence = true;
        timeControl.IncidenceDescription = request.IncidenceDescription;
        await timeControlRepository.UpdateAsync(timeControl, cancellationToken);

        // Notificar a SignalR de nueva incidencia.
        await hubContext.Clients.All.SendAsync(HubNames.TimeControlIncidences, cancellationToken);

        return Result.Success();
    }
}
