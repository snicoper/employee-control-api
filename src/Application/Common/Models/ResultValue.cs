namespace EmployeeControl.Application.Common.Models;

public sealed class ResultValue<TValue> : Result
{
    private ResultValue(TValue? value, bool succeeded, IEnumerable<string> errors)
        : base(succeeded, errors)
    {
        Value = value;
    }

    public TValue? Value { get; set; }

    public static ResultValue<TValue> Success(TValue value)
    {
        return new ResultValue<TValue>(value, true, []);
    }

    public static new ResultValue<TValue> Failure(IEnumerable<string> errors)
    {
        return new ResultValue<TValue>(default, false, errors);
    }

    public static new ResultValue<TValue> Failure(string error)
    {
        return new ResultValue<TValue>(default, false, [error]);
    }

    public static new ResultValue<TValue> Failure()
    {
        return new ResultValue<TValue>(default, false, [string.Empty]);
    }
}
