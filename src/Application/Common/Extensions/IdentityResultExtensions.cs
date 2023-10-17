﻿using EmployeeControl.Application.Common.Models;
using Microsoft.AspNetCore.Identity;

namespace EmployeeControl.Application.Common.Extensions;

public static class IdentityResultExtensions
{
    public static Result ToResult(this IdentityResult result)
    {
        return result.Succeeded
            ? Result.Success()
            : Result.Failure(result.Errors.Select(e => e.Description));
    }

    public static ResultValue<TValue> ToResultValue<TValue>(this IdentityResult result, TValue value)
    {
        return result.Succeeded
            ? ResultValue<TValue>.Success(value)
            : ResultValue<TValue>.Failure(result.Errors.Select(e => e.Description));
    }
}
