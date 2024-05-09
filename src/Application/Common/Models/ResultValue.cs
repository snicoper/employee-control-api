using FluentValidation.Results;

namespace EmployeeControl.Application.Common.Models;

public sealed class ResultValue<TValue> : Result
{
    private ResultValue(TValue? value, bool succeeded, ICollection<ValidationFailure> errors)
        : base(succeeded, errors)
    {
        Value = value;
    }

    public TValue? Value { get; set; }

    public static ResultValue<TValue> Success(TValue value)
    {
        return new ResultValue<TValue>(value, true, []);
    }

    public static new ResultValue<TValue> Failure(ICollection<ValidationFailure> errors)
    {
        return new ResultValue<TValue>(default, false, errors);
    }

    public static new ResultValue<TValue> Failure(ValidationFailure validationFailure)
    {
        return Failure([validationFailure]);
    }

    public static new ResultValue<TValue> Failure(string code, string error)
    {
        var validationFailure = new ValidationFailure(code, error);

        return Failure(validationFailure);
    }
}
