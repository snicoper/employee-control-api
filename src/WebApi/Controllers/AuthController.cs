using EmployeeControl.Application.Features.Auth.Commands.Login;
using EmployeeControl.Application.Features.Auth.Commands.RefreshToken;
using EmployeeControl.WebApi.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeControl.WebApi.Controllers;

[Route("api/v{version:apiVersion}/auth")]
public class AuthController : ApiControllerBase
{
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
    public async Task<ActionResult<RefreshTokenDto>> RefreshToken(RefreshTokenCommand command)
    {
        return await Mediator.Send(command);
    }
}
