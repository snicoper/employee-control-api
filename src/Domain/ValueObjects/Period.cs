using EmployeeControl.Domain.Common;

namespace EmployeeControl.Domain.ValueObjects;

public class Period : ValueObject
{
    private Period(DateTimeOffset start, DateTimeOffset end)
    {
        Start = start;
        End = end;
    }

    public DateTimeOffset Start { get; }

    public DateTimeOffset End { get; }

    public static Period Create(DateTimeOffset start, DateTimeOffset end)
    {
        var timeInterval = new Period(start, end);

        return timeInterval;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Start;
        yield return End;
    }
}
