using EmployeeControl.Application.Common.Interfaces.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace EmployeeControl.Infrastructure.Hubs;

public class NotificationTimeControlIncidenceHub : Hub<INotificationTimeControlIncidenceHub>
{
    public async Task SendMessageAsync(CancellationToken cancellationToken)
    {
        await Clients.All.SendMessageAsync("NotificationTimeControlIncidenceHub", cancellationToken);
    }
}
