using EmployeeControl.Application.Common.Exceptions;
using EmployeeControl.Domain.Common;

namespace EmployeeControl.Application.Common.Extensions;

public static class ResultExtensions
{
    public static void RaiseBadRequest(this Result result)
    {
        Raise(result.Succeeded, result.Errors.ToDictionary());
    }

    public static void RaiseBadRequest<TResult>(this Result<TResult> result)
    {
        Raise(result.Succeeded, result.Errors.ToDictionary());
    }

    public static void Raise(bool succeeded, Dictionary<string, string[]> errors)
    {
        if (succeeded)
        {
            return;
        }

        throw new CustomValidationException(errors);
    }
}
