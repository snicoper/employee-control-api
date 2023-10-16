using EmployeeControl.Application.Features.Identity.Commands.RegisterIdentity;
using EmployeeControl.WebApi.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeControl.WebApi.Controllers;

[Route("api/v{version:apiVersion}/identity")]
public class IdentityController : ApiControllerBase
{
    /// <summary>
    /// Creación de una cuenta por el usuario.
    /// </summary>
    /// <param name="command">Datos del usuario.</param>
    /// <returns>Id del usuario creado en caso de éxito.</returns>
    [AllowAnonymous]
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> RegisterIdentity(RegisterIdentityCommand command)
    {
        var result = await Mediator.Send(command);

        return ObjectResultCreated(result);
    }
}
