using EmployeeControl.Application.Common.Exceptions;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;

namespace EmployeeControl.Application.Common.Models;

public class Result
{
    protected Result(bool succeeded, ICollection<ValidationFailure> errors)
    {
        Succeeded = succeeded;
        Errors = errors;
        Status = StatusCodes.Status200OK;
    }

    public bool Succeeded { get; private set; }

    public int Status { get; set; }

    public ICollection<ValidationFailure> Errors { get; }

    public static Result Create()
    {
        return new Result(false, []);
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

    public static Result Failure(ICollection<ValidationFailure> errors)
    {
        return new Result(false, errors);
    }

    public static Result<TValue> Failure<TValue>(ICollection<ValidationFailure> errors)
    {
        return new Result<TValue>(default, false, errors);
    }

    public static Result Failure(ValidationFailure validationFailure)
    {
        return Failure([validationFailure]);
    }

    public static Result<TValue> Failure<TValue>(ValidationFailure validationFailure)
    {
        return Failure<TValue>([validationFailure]);
    }

    public static Result Failure(string code, string error)
    {
        var validationFailure = new ValidationFailure(code, error);

        return Failure(validationFailure);
    }

    public static Result<TValue> Failure<TValue>(string code, string error)
    {
        var validationFailure = new ValidationFailure(code, error);

        return Failure<TValue>(validationFailure);
    }

    public void AddValidationFailure(ValidationFailure validationFailure)
    {
        Succeeded = false;
        Status = StatusCodes.Status400BadRequest;

        Errors.Add(validationFailure);
    }

    public void Add(string code, string error)
    {
        AddValidationFailure(new ValidationFailure(code, error));
    }

    public void RaiseBadRequestIfResultFailure()
    {
        if (Succeeded)
        {
            return;
        }

        throw new CustomValidationException([.. Errors]);
    }
}

#pragma warning disable SA1402 // File may only contain a single type
public class Result<TValue> : Result
{
    protected internal Result(TValue? value, bool succeeded, ICollection<ValidationFailure> errors)
        : base(succeeded, errors)
    {
        Value = value;
    }

    public TValue? Value { get; set; }
}
#pragma warning disable SA1402 // File may only contain a single type
