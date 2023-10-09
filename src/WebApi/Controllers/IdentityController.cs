using EmployeeControl.Application.Features.Identity.Commands.Login;
using EmployeeControl.Application.Features.Identity.Commands.RefreshToken;
using EmployeeControl.Application.Features.Identity.Commands.Register;
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
    public async Task<ActionResult<string>> Register(RegisterCommand command)
    {
        return await Mediator.Send(command);
    }

    /// <summary>
    /// Identificación de usuario.
    /// </summary>
    /// <param name="command">Datos de usuario.</param>
    /// <returns>Token y refresh token en caso de éxito.</returns>
    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<LoginDto>> Login(LoginCommand command)
    {
        return await Mediator.Send(command);
    }

    /// <summary>
    /// Obtener nuevo token y refresh token.
    /// </summary>
    /// <param name="command">Actual refresh token del usuario.</param>
    /// <returns>Token y refresh token en caso de éxito.</returns>
    [AllowAnonymous]
    [HttpPost("refresh-token")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<RefreshTokenDto>> Login(RefreshTokenCommand command)
    {
        return await Mediator.Send(command);
    }
}
