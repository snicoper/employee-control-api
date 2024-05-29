using EmployeeControl.Application.Common.Constants;
using EmployeeControl.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace EmployeeControl.Application.Common.Extensions;

public static class IdentityResultExtensions
{
    public static Result ToResult(this IdentityResult result)
    {
        var identityErrors = result
            .Errors
            .Select(e => e.Description)
            .ToArray();

        return result.Succeeded
            ? Result.Success()
            : Result.Failure(ValidationErrorsKeys.IdentityError, identityErrors);
    }
}
