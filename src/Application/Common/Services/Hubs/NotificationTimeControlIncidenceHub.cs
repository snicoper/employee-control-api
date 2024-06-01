using EmployeeControl.Application.Common.Constants;
using Microsoft.AspNetCore.SignalR;

namespace EmployeeControl.Application.Common.Services.Hubs;

public class NotificationTimeControlIncidenceHub : Hub
{
    public async Task Notify()
    {
        await Clients.All.SendAsync(HubNames.TimeControlIncidences);
    }
}
