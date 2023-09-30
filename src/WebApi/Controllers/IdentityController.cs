using EmployeeControl.Application.Cqrs.Identity.Commands.CreateAccount;
using EmployeeControl.Application.Cqrs.Identity.Commands.Login;
using EmployeeControl.WebApi.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeControl.WebApi.Controllers;

[Route("api/v{version:apiVersion}/identity")]
public class IdentityController : ApiControllerBase
{
    /// <summary>
    ///     Creación de una cuenta por el usuario.
    /// </summary>
    /// <param name="command">Datos del usuario.</param>
    /// <returns>Id del usuario creado en caso de éxito.</returns>
    [AllowAnonymous]
    [HttpPost("create-account")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> CreateAccount(CreateAccountCommand command)
    {
        return await Mediator.Send(command);
    }

    /// <summary>
    ///     Identificación de un usuario.
    /// </summary>
    /// <param name="command">Datos de usuario.</param>
    /// <returns>Token en caso de éxito.</returns>
    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<LoginDto>> Login(LoginCommand command)
    {
        return await Mediator.Send(command);
    }
}
