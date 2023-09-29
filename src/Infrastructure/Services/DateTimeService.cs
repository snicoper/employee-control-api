using EmployeeControl.Application.Common.Interfaces;

namespace EmployeeControl.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.UtcNow;
}
