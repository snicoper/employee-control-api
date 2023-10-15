using EmployeeControl.Application.Common.Interfaces;

namespace EmployeeControl.Infrastructure.Services;

public class DateTimeService : IDateTimeService
{
    public DateTime Now => DateTime.Now;

    public DateTime UtcNow => DateTime.UtcNow;
}
