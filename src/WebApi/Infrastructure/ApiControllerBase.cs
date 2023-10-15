using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeControl.WebApi.Infrastructure;

[ApiController]
[Authorize]
[Produces("application/json")]
public class ApiControllerBase : ControllerBase
{
    private ISender? _mediator;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    protected ObjectResult ObjectResult201<TResult>(TResult result)
    {
        return new ObjectResult(result) { StatusCode = StatusCodes.Status201Created };
    }
}
