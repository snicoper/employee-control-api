namespace EmployeeControl.Application.Common.Interfaces.Common;

public interface IDateTimeService
{
    DateTime Now { get; }

    DateTime UtcNow { get; }
}
