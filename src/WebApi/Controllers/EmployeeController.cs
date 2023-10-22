using EmployeeControl.Application.Common.Models;
using EmployeeControl.Application.Features.Employee.Commands.InviteEmployee;
using EmployeeControl.WebApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeControl.WebApi.Controllers;

[Route("api/v{version:apiVersion}/employee")]
public class EmployeeController : ApiControllerBase
{
    /// <summary>
    /// Invitar a un empleado.
    /// </summary>
    /// <param name="command">Datos del empleado.</param>
    /// <returns>Id del empleado creado en caso de éxito.</returns>
    [HttpPost("invite")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result>> InviteEmployee(InviteEmployeeCommand command)
    {
        var result = await Sender.Send(command);

        return result;
    }
}
