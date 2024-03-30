namespace EmployeeControl.Application.Common.Interfaces.Hubs;

public interface INotificationTimeControlIncidenceHub
{
    Task SendMessageAsync(string method, CancellationToken cancellationToken);
}
