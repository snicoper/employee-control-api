using EmployeeControl.Application.Common.Constants;
using EmployeeControl.Application.Common.Models;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;

namespace EmployeeControl.Application.Common.Extensions;

public static class IdentityResultExtensions
{
    public static Result ToApplicationResult(this IdentityResult result)
    {
        var identityErrors = result
            .Errors
            .Select(e => e.Description)
            .Select(error => new ValidationFailure(ValidationErrorsKeys.IdentityError, error))
            .ToList();

        return result.Succeeded
            ? Result.Success()
            : Result.Failure([.. identityErrors]);
    }
}
