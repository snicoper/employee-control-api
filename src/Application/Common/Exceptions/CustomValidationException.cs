using FluentValidation.Results;

namespace EmployeeControl.Application.Common.Exceptions;

[Serializable]
public class CustomValidationException() : Exception("One or more validation failures have occurred.")
{
    public CustomValidationException(IEnumerable<ValidationFailure> failures)
        : this()
    {
        Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }

    public IDictionary<string, string[]> Errors { get; } = new Dictionary<string, string[]>();
}
