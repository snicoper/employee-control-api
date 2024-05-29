using EmployeeControl.Domain.Enums;

namespace EmployeeControl.Application.Features.TimesControl.Queries.GetTimeControlRangeByEmployeeId;

public record GetTimeControlRangeByEmployeeIdResponse
{
    public int Day { get; set; }

    public string? DayTitle { get; set; }

    public int TotalMinutes { get; set; }

    public ICollection<TimeControlResponse> Times { get; set; } = [];

    public record TimeControlResponse(
        Guid Id,
        DateTimeOffset Start,
        DateTimeOffset Finish,
        bool Incidence,
        TimeState TimeState,
        ClosedBy ClosedBy,
        int Minutes,
        double DayPercent);
}
