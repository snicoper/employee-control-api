using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeControl.WebApi.Controllers;

[ApiController]
[Produces("application/json")]
public class ApiControllerBase : ControllerBase
{
    private ISender? _mediator;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}
