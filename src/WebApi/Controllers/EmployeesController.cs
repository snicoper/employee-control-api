using EmployeeControl.Application.Common.Models;
using EmployeeControl.Application.Features.Employees.Commands.InviteEmployee;
using EmployeeControl.Application.Features.Employees.Queries.GetEmployeesPaginated;
using EmployeeControl.WebApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeControl.WebApi.Controllers;

[Route("api/v{version:apiVersion}/employees")]
public class EmployeesController : ApiControllerBase
{
    /// <summary>
    /// Obtener lista de empleados paginados.
    /// </summary>
    /// <param name="request">RequestData.</param>
    /// <returns>Lista de empleados paginádos.</returns>
    [HttpPost("paginated")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ResponseData<GetEmployeesPaginatedResponse>>> GetEmployeesPaginated(
        RequestData request)
    {
        var result = await Sender.Send(new GetEmployeesPaginatedQuery(request));

        return result;
    }

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

        return ObjectResultWithStatusCode(result, StatusCodes.Status201Created);
    }
}
