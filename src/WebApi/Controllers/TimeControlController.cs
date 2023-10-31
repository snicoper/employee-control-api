using EmployeeControl.Application.Common.Models;
using EmployeeControl.Application.Features.TimeControl.Commands.StartTimeControl;
using EmployeeControl.Application.Features.TimeControl.Queries.GetCurrentStateTimeControl;
using EmployeeControl.Domain.Entities;
using EmployeeControl.WebApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeControl.WebApi.Controllers;

[Route("api/v{version:apiVersion}/time-control")]
public class TimeControlController : ApiControllerBase
{
    /// <summary>
    /// Obtener el estado actual.
    /// <para>Si tiene inicializado un <see cref="TimeControl" />, el estado es true.</para>
    /// </summary>
    /// <param name="employeeId">Id empleado a comprobar.</param>
    /// <returns>IsOpen, true si tiene abierto un tiempo, false en caso contrario.</returns>
    [HttpGet("employees/{employeeId}/state")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetCurrentStateTimeControlResponse>> GetCurrentStateTimeControl(string employeeId)
    {
        var result = await Sender.Send(new GetCurrentStateTimeControlQuery(employeeId));

        return result;
    }

    /// <summary>
    /// Inicializa un TimeControl.
    /// </summary>
    /// <param name="command">Employee Id.</param>
    /// <returns>Result con el estado del proceso.</returns>
    [HttpPost("start")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result>> StartTimeControl(StartTimeControlCommand command)
    {
        var result = await Sender.Send(command);

        return result;
    }
}
