namespace EmployeeControl.Domain.Common;

public class Result
{
    protected Result(bool succeeded, Dictionary<string, string[]> errors)
    {
        Succeeded = succeeded;
        Errors = errors;
    }

    public bool Succeeded { get; private set; }

    public int Status { get; set; }

    public IDictionary<string, string[]> Errors { get; }

    public static Result Create()
    {
        return new Result(true, []);
    }

    public static Result<TValue> Create<TValue>(TValue? value)
    {
        return new Result<TValue>(value, true, []);
    }

    public static Result Success()
    {
        return new Result(true, []);
    }

    public static Result<TValue> Success<TValue>(TValue? value)
    {
        return new Result<TValue>(value, true, []);
    }

    public static Result Failure(Dictionary<string, string[]> errors)
    {
        return new Result(false, errors);
    }

    public static Result<TValue> Failure<TValue>(Dictionary<string, string[]> errors)
    {
        return new Result<TValue>(default, false, errors);
    }

    public static Result Failure(string code, string[] errors)
    {
        var result = Create();
        result.Errors.Add(code, errors);
        result.Succeeded = false;

        return result;
    }

    public static Result<TValue> Failure<TValue>(string code, string[] errors)
    {
        var result = Create<TValue>(default);
        result.Errors.Add(code, errors);
        result.Succeeded = false;

        return result;
    }

    public static Result Failure(string code, string error)
    {
        var result = Create();
        result.AddError(code, error);
        result.Succeeded = false;

        return result;
    }

    public static Result<TValue> Failure<TValue>(string code, string error)
    {
        var result = Create<TValue>(default);
        result.AddError(code, error);
        result.Succeeded = false;

        return result;
    }

    public void AddError(string code, string error)
    {
        Succeeded = false;
        Errors.Add(code, [error]);
    }
}

#pragma warning disable SA1402 // File may only contain a single type
public class Result<TValue> : Result
{
    protected internal Result(TValue? value, bool succeeded, Dictionary<string, string[]> errors)
        : base(succeeded, errors)
    {
        Value = value;
    }

    public TValue? Value { get; }

    public static implicit operator Result<TValue>(TValue? value)
    {
        return Create(value);
    }
}
#pragma warning disable SA1402 // File may only contain a single type
