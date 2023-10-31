using EmployeeControl.Application.Common.Models;
using EmployeeControl.Application.Features.TimeControl.Commands.StartTimeControl;
using EmployeeControl.WebApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeControl.WebApi.Controllers;

[Route("api/v{version:apiVersion}/time-control")]
public class TimeControlController : ApiControllerBase
{
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
