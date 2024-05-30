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
        return new Period(start, end);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Start;
        yield return End;
    }
}
