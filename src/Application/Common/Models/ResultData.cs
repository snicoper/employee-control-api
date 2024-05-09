namespace EmployeeControl.Application.Common.Models;

public sealed class ResultData<TValue> : Result
{
    private ResultData(TValue? value, bool succeeded, IEnumerable<string> errors)
        : base(succeeded, errors)
    {
        Value = value;
    }

    public TValue? Value { get; set; }

    public static ResultData<TValue> Success(TValue data)
    {
        return new ResultData<TValue>(data, true, []);
    }

    public static new ResultData<TValue> Failure(IEnumerable<string> errors)
    {
        return new ResultData<TValue>(default, false, errors);
    }

    public static new ResultData<TValue> Failure(string error)
    {
        return new ResultData<TValue>(default, false, [error]);
    }

    public static new ResultData<TValue> Failure()
    {
        return new ResultData<TValue>(default, false, [string.Empty]);
    }
}
