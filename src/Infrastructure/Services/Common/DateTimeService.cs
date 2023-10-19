using EmployeeControl.Application.Common.Interfaces.Common;

namespace EmployeeControl.Infrastructure.Services.Common;

public class DateTimeService : IDateTimeService
{
    public DateTime Now => DateTime.Now;

    public DateTime UtcNow => DateTime.UtcNow;
}
