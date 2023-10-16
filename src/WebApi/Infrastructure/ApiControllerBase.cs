﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeControl.WebApi.Infrastructure;

[ApiController]
[Authorize]
[Produces("application/json")]
public class ApiControllerBase : ControllerBase
{
    private ISender? _sender;

    protected ISender Sender => _sender ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    protected ObjectResult ObjectResultCreated<TResult>(TResult result)
    {
        return new ObjectResult(result) { StatusCode = StatusCodes.Status201Created };
    }
}
