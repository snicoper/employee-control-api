using EmployeeControl.Application.Common.Extensions;
using FluentValidation.Results;

namespace EmployeeControl.Application.Common.Exceptions;

[Serializable]
public class CustomValidationException : Exception
{
    public CustomValidationException()
        : base("One or more validation failures have occurred.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public CustomValidationException(IEnumerable<ValidationFailure> failures)
        : this()
    {
        Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key.LowerCaseFirst(), failureGroup => failureGroup.ToArray());
    }

    public IDictionary<string, string[]> Errors { get; }
}
