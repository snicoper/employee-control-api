using EmployeeControl.Application.Common.Exceptions;
using EmployeeControl.Application.Common.Interfaces.Validation;
using EmployeeControl.Application.Common.Models;
using FluentValidation.Results;

namespace EmployeeControl.Infrastructure.Services.Validation;

public class ValidationResultService : IValidationResultService
{
    private List<ValidationFailure> Errors { get; } =
    [
    ];

    public bool HasErrors()
    {
        return Errors.Count != 0;
    }

    public int ErrorsCount()
    {
        return Errors.Count;
    }

    public void Add(string property, string error)
    {
        Errors.Add(new ValidationFailure(property, error));
    }

    public void Add(Dictionary<string, string> errors)
    {
        foreach (var (key, value) in errors)
        {
            Add(key, value);
        }
    }

    public void AddAndRaiseException(string key, string value)
    {
        Add(key, value);
        RaiseException();
    }

    public void AddAndRaiseException(Dictionary<string, string> errors)
    {
        foreach (var (key, value) in errors)
        {
            Add(key, value);
        }

        RaiseException();
    }

    public void RaiseException()
    {
        throw new CustomValidationException(Errors);
    }

    public void RaiseExceptionIfExistsErrors()
    {
        if (HasErrors())
        {
            RaiseException();
        }
    }

    public Result ToResult()
    {
        return HasErrors() ? Result.Failure(Errors) : Result.Success();
    }

    public Result<TValue> ToResult<TValue>(TValue value)
    {
        return HasErrors() ? Result.Failure<TValue>(Errors) : Result.Success(value);
    }
}
