using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeControl.WebApi.Infrastructure;

[ApiController]
[Authorize]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public class ApiControllerBase : ControllerBase
{
    private ISender? sender;

    protected ISender Sender => sender ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    /// <summary>
    /// Devolver un resultado con un StatusCode concreto.
    /// </summary>
    /// <param name="result">Resultado a devolver.</param>
    /// <param name="statusCode">StatusCode respuesta.</param>
    /// <typeparam name="TResult">Tipo a devolver.</typeparam>
    protected static ObjectResult ObjectResultWithStatusCode<TResult>(TResult result, int statusCode)
    {
        return new ObjectResult(result) { StatusCode = statusCode };
    }
}
