using EmployeeControl.Application.Common.Models;
using EmployeeControl.Application.Features.TimesControl.Commands.FinishTimeControl;
using EmployeeControl.Application.Features.TimesControl.Commands.StartTimeControl;
using EmployeeControl.Application.Features.TimesControl.Queries.GetTimeControlRangeByEmployeeId;
using EmployeeControl.Application.Features.TimesControl.Queries.GetTimeStateByEmployeeId;
using EmployeeControl.Domain.Entities;
using EmployeeControl.WebApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeControl.WebApi.Controllers;

[Route("api/v{version:apiVersion}/times-control")]
public class TimesControlController : ApiControllerBase
{
    /// <summary>
    /// Obtener una lista de <see cref="TimeControl" /> de un empleado concreto
    /// en un rango de fechas.
    /// </summary>
    /// <param name="employeeId">Id empleado a comprobar.</param>
    /// <param name="from">Fecha inicial.</param>
    /// <param name="to">Fecha final.</param>
    /// <returns>IsOpen, true si tiene abierto un tiempo, false en caso contrario.</returns>
    [HttpGet("employees/{employeeId}/from/{from}/to{to}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ICollection<GetTimeControlRangeByEmployeeIdResponse>>>
        GetTimeControlRangeByEmployeeId(string employeeId, DateTimeOffset from, DateTimeOffset to)
    {
        var result = await Sender.Send(new GetTimeControlRangeByEmployeeIdQuery(employeeId, from, to));

        return result.ToList();
    }

    /// <summary>
    /// Obtener el estado actual de un empleado por su Id.
    /// <para>Si tiene inicializado un <see cref="TimeControl" />, el estado es true.</para>
    /// </summary>
    /// <param name="employeeId">Id empleado.</param>
    /// <returns>IsOpen, true si tiene abierto un tiempo, false en caso contrario.</returns>
    [HttpGet("employees/{employeeId}/time-state")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetTimeStateByEmployeeIdResponse>> GetTimeStateByEmployeeId(string employeeId)
    {
        var result = await Sender.Send(new GetTimeStateByEmployeeIdQuery(employeeId));

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

    /// <summary>
    /// Finalizar un TimeControl.
    /// </summary>
    /// <param name="command">Employee Id.</param>
    /// <returns>Result con el estado del proceso.</returns>
    [HttpPost("finish")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result>> FinishTimeControl(FinishTimeControlCommand command)
    {
        var result = await Sender.Send(command);

        return result;
    }
}
