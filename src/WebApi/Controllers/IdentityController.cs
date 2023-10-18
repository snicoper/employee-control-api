using EmployeeControl.Application.Common.Models;
using EmployeeControl.Application.Features.Identity.Commands.RegisterIdentity;
using EmployeeControl.Application.Features.Identity.Commands.RegisterValidateEmail;
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
        var result = await Sender.Send(command);

        return ObjectResultCreated(result);
    }

    /// <summary>
    /// Validación de email para una cuenta creada de cero.
    /// </summary>
    /// <param name="command">Datos del Code y UserId del usuario a validar.</param>
    /// <returns>Unit.</returns>
    [AllowAnonymous]
    [HttpPost("validate-email")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result>> RegisterValidateEmail(RegisterValidateEmailCommand command)
    {
        var result = await Sender.Send(command);

        return result;
    }
}
