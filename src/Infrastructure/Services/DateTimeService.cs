using EmployeeControl.Application.Common.Interfaces;

namespace EmployeeControl.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;

    public DateTime UtcNow => DateTime.UtcNow;
}
