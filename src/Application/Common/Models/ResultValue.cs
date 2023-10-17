namespace EmployeeControl.Application.Common.Models;

public class ResultValue<TValue> : Result
{
    private ResultValue(bool succeeded, IEnumerable<string> errors)
        : base(succeeded, errors)
    {
    }

    private ResultValue(TValue value, bool succeeded, IEnumerable<string> errors)
        : base(succeeded, errors)
    {
        Value = value;
    }

    public TValue? Value { get; private set; }

    public static ResultValue<TValue> Success(TValue value)
    {
        return new ResultValue<TValue>(value, true, Array.Empty<string>());
    }

    public static new ResultValue<TValue> Failure(IEnumerable<string> errors)
    {
        return new ResultValue<TValue>(false, errors);
    }
}
