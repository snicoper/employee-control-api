using EmployeeControl.Application.Common.Exceptions;
using FluentValidation.Results;

namespace EmployeeControl.Application.Common.Models;

public class Result
{
    protected Result(bool succeeded, ICollection<ValidationFailure> errors)
    {
        Succeeded = succeeded;
        Errors = errors;
    }

    public bool Succeeded { get; private set; }

    public ICollection<ValidationFailure> Errors { get; }

    public static Result Success()
    {
        return new Result(true, []);
    }

    public static Result Failure(ICollection<ValidationFailure> errors)
    {
        return new Result(false, errors);
    }

    public static Result Failure(ValidationFailure validationFailure)
    {
        return Failure([validationFailure]);
    }

    public static Result Failure(string code, string error)
    {
        var validationFailure = new ValidationFailure(code, error);

        return Failure(validationFailure);
    }

    public void AddValidationFailure(ValidationFailure validationFailure)
    {
        Succeeded = false;
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
