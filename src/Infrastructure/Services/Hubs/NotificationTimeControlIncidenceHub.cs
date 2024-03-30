using EmployeeControl.Application.Common.Interfaces.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace EmployeeControl.Infrastructure.Services.Hubs;

public class NotificationTimeControlIncidenceHub : Hub, INotificationTimeControlIncidenceHub
{
    public Task SendMessage()
    {
        return Clients.All.SendAsync("notification-time-control-incidence", true);
    }
}
