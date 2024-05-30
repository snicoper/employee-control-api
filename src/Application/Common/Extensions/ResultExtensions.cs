using EmployeeControl.Application.Common.Exceptions;
using EmployeeControl.Domain.Common;

namespace EmployeeControl.Application.Common.Extensions;

public static class ResultExtensions
{
    public static void RaiseBadRequestIfErrorsExist(this Result result)
    {
        RaiseBadRequestExceptionIfErrorsExist(result.Succeeded, result.Errors);
    }

    public static void RaiseBadRequest<TResult>(this Result<TResult> result)
    {
        RaiseBadRequestExceptionIfErrorsExist(result.Succeeded, result.Errors);
    }

    private static void RaiseBadRequestExceptionIfErrorsExist(bool succeeded, IDictionary<string, string[]> errors)
    {
        if (succeeded)
        {
            return;
        }

        throw new BadRequestException(errors);
    }
}
