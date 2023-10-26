using EmployeeControl.Application.Common.Models;
using EmployeeControl.Application.Features.Employees.Commands.ActivateEmployee;
using EmployeeControl.Application.Features.Employees.Commands.AddRoleHumanResources;
using EmployeeControl.Application.Features.Employees.Commands.DeactivateEmployee;
using EmployeeControl.Application.Features.Employees.Commands.InviteEmployee;
using EmployeeControl.Application.Features.Employees.Commands.RemoveRoleHumanResources;
using EmployeeControl.Application.Features.Employees.Commands.UpdateEmployee;
using EmployeeControl.Application.Features.Employees.Queries.GetEmployeeById;
using EmployeeControl.Application.Features.Employees.Queries.GetEmployeesPaginated;
using EmployeeControl.Application.Features.Employees.Queries.GetRolesByEmployeeId;
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
    [HttpGet("paginated")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ResponseData<GetEmployeesPaginatedResponse>>> GetEmployeesPaginated(
        [FromQuery] RequestData request)
    {
        var result = await Sender.Send(new GetEmployeesPaginatedQuery(request));

        return result;
    }

    /// <summary>
    /// Obtener empleado por su Id.
    /// </summary>
    /// <param name="id">Id empleado.</param>
    /// <returns>Datos del empleado.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<GetEmployeeByIdResponse>> GetEmployeeById(string id)
    {
        var result = await Sender.Send(new GetEmployeeByIdQuery(id));

        return result;
    }

    /// <summary>
    /// Obtener roles de un empleado por el employeeId.
    /// </summary>
    /// <param name="id">Id empleado.</param>
    /// <returns>Roles del empleado.</returns>
    [HttpGet("{id}/roles")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ICollection<GetRolesByEmployeeIdResponse>>> GetRolesByEmployeeId(string id)
    {
        var result = await Sender.Send(new GetRolesByEmployeeIdQuery(id));

        return result.ToList();
    }

    /// <summary>
    /// Invitar a un empleado.
    /// </summary>
    /// <param name="command">Datos del empleado.</param>
    /// <returns>Id del empleado creado en caso de éxito.</returns>
    [HttpPost("invite")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> InviteEmployee(InviteEmployeeCommand command)
    {
        var result = await Sender.Send(command);

        return ObjectResultWithStatusCode(result, StatusCodes.Status201Created);
    }

    /// <summary>
    /// Desactivar un empleado.
    /// </summary>
    /// <param name="command">Datos del empleado a desactivar.</param>
    /// <returns>Result.</returns>
    [HttpPost("deactivate")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result>> DeactivateEmployee(DeactivateEmployeeCommand command)
    {
        var result = await Sender.Send(command);

        return result;
    }

    /// <summary>
    /// Activar un empleado.
    /// </summary>
    /// <param name="command">Datos del empleado a activar.</param>
    /// <returns>Result.</returns>
    [HttpPost("activate")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result>> ActivateEmployee(ActivateEmployeeCommand command)
    {
        var result = await Sender.Send(command);

        return result;
    }

    /// <summary>
    /// Añadir rol de RRHH a un empleado.
    /// </summary>
    /// <param name="command">Id empleado.</param>
    /// <returns>Result.</returns>
    [HttpPost("{id}/add-role-rrhh")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result>> AddRoleHumanResources(AddRoleHumanResourcesCommand command)
    {
        var result = await Sender.Send(command);

        return result;
    }

    /// <summary>
    /// Eliminar rol de RRHH a un empleado.
    /// </summary>
    /// <param name="command">Id empleado.</param>
    /// <returns>Result.</returns>
    [HttpPost("{id}/remove-role-rrhh")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result>> RemoveRoleHumanResources(RemoveRoleHumanResourcesCommand command)
    {
        var result = await Sender.Send(command);

        return result;
    }

    /// <summary>
    /// Actualizar datos de empleado.
    /// </summary>
    /// <param name="command">Datos del empleado a actualizar.</param>
    /// <returns>Result.</returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result>> UpdateEmployee(UpdateEmployeeCommand command)
    {
        var result = await Sender.Send(command);

        return result;
    }
}
