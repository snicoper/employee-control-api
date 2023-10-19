using EmployeeControl.Application.Common.Models;
using EmployeeControl.Application.Features.Identity.Commands.EmailValidationForwarding;
using EmployeeControl.Application.Features.Identity.Commands.RecoveryPassword;
using EmployeeControl.Application.Features.Identity.Commands.RecoveryPasswordChange;
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
    /// <returns>Result.</returns>
    [AllowAnonymous]
    [HttpPost("validate-email")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result>> RegisterValidateEmail(RegisterValidateEmailCommand command)
    {
        var result = await Sender.Send(command);

        return result;
    }

    /// <summary>
    /// Re-envía una validación de email para una cuenta creada.
    /// </summary>
    /// <param name="command">Datos del UserId del usuario a validar.</param>
    /// <returns>Result.</returns>
    [AllowAnonymous]
    [HttpPost("email-validation-forwarding")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result>> EmailValidationForwarding(EmailValidationForwardingCommand command)
    {
        var result = await Sender.Send(command);

        return result;
    }

    /// <summary>
    /// Recordar contraseña, envía un email para restablecer una nueva contraseña.
    /// </summary>
    /// <param name="command">Datos del UserId del usuario a validar.</param>
    /// <returns>Result.</returns>
    [AllowAnonymous]
    [HttpPost("recovery-password")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result>> RecoveryPassword(RecoveryPasswordCommand command)
    {
        var result = await Sender.Send(command);

        return result;
    }

    /// <summary>
    /// Restablece una contraseña olvidada.
    /// </summary>
    /// <param name="command">Datos para restablecer la contraseña.</param>
    /// <returns>Result.</returns>
    [AllowAnonymous]
    [HttpPost("recovery-password-change")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result>> RecoveryPasswordChange(RecoveryPasswordChangeCommand command)
    {
        var result = await Sender.Send(command);

        return result;
    }
}
