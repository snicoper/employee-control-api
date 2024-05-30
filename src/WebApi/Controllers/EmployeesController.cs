using EmployeeControl.Application.Common.Models;
using EmployeeControl.Application.Features.Employees.Commands.ActivateEmployee;
using EmployeeControl.Application.Features.Employees.Commands.AddRoleHumanResources;
using EmployeeControl.Application.Features.Employees.Commands.DeactivateEmployee;
using EmployeeControl.Application.Features.Employees.Commands.InviteEmployee;
using EmployeeControl.Application.Features.Employees.Commands.RemoveRoleHumanResources;
using EmployeeControl.Application.Features.Employees.Commands.UpdateEmployee;
using EmployeeControl.Application.Features.Employees.Commands.UpdateEmployeeRoles;
using EmployeeControl.Application.Features.Employees.Commands.UpdateEmployeeSettings;
using EmployeeControl.Application.Features.Employees.Queries.GetCurrentEmployee;
using EmployeeControl.Application.Features.Employees.Queries.GetCurrentEmployeeSettings;
using EmployeeControl.Application.Features.Employees.Queries.GetEmployeeById;
using EmployeeControl.Application.Features.Employees.Queries.GetEmployeesPaginated;
using EmployeeControl.Application.Features.Employees.Queries.GetRolesByEmployeeId;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Entities;
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
    /// <returns>Lista de empleados paginados.</returns>
    [HttpGet("paginated")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<ResponseData<GetEmployeesPaginatedResponse>>>> GetEmployeesPaginated(
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
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<GetEmployeeByIdResponse>>> GetEmployeeById(Guid id)
    {
        var result = await Sender.Send(new GetEmployeeByIdQuery(id));

        return result;
    }

    /// <summary>
    /// Obtener roles de un empleado por el Id de empleado.
    /// </summary>
    /// <param name="id">Id empleado.</param>
    /// <returns>Roles del empleado.</returns>
    [HttpGet("{id:guid}/roles")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<ICollection<GetRolesByEmployeeIdResponse>>>> GetRolesByEmployeeId(Guid id)
    {
        var result = await Sender.Send(new GetRolesByEmployeeIdQuery(id));

        return result;
    }

    /// <summary>
    /// Obtener el empleado actual.
    /// </summary>
    /// <returns>Empleado actual.</returns>
    [HttpGet("current")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<GetCurrentEmployeeResponse>>> GetCurrentEmployee()
    {
        var result = await Sender.Send(new GetCurrentEmployeeQuery());

        return result;
    }

    /// <summary>
    /// Obtener configuración del empleado actual.
    /// </summary>
    /// <returns>Configuración del empleado.</returns>
    [HttpGet("settings")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<GetCurrentEmployeeSettingsResponse>>> GetCurrentEmployeeSettings()
    {
        var result = await Sender.Send(new GetCurrentEmployeeSettingsQuery());

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
    public async Task<ActionResult<Result<string>>> InviteEmployee(InviteEmployeeCommand command)
    {
        var result = await Sender.Send(command);

        return ResultWithStatus(result, StatusCodes.Status201Created);
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

    /// <summary>
    /// Actualizar los roles de un empleado.
    /// </summary>
    /// <param name="command">Lista de roles a asignar.</param>
    /// <returns>Roles del empleado.</returns>
    [HttpPut("{id:guid}/roles")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result>> UpdateEmployeeRoles(UpdateEmployeeRolesCommand command)
    {
        var result = await Sender.Send(command);

        return result;
    }

    /// <summary>
    /// Desactivar un empleado.
    /// </summary>
    /// <param name="command">Datos del empleado a desactivar.</param>
    /// <returns>Result.</returns>
    [HttpPut("{id:guid}/deactivate")]
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
    [HttpPut("{id:guid}/activate")]
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
    [HttpPut("{id:guid}/add-role-rrhh")]
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
    [HttpPut("{id:guid}/remove-role-rrhh")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result>> RemoveRoleHumanResources(RemoveRoleHumanResourcesCommand command)
    {
        var result = await Sender.Send(command);

        return result;
    }

    /// <summary>
    /// Actualizar configuración de empleado.
    /// </summary>
    /// <param name="command">Datos a actualizar.</param>
    /// <returns><see cref="EmployeeSettings" /> con los datos actualizados.</returns>
    [HttpPut("{id:guid}/employee-settings")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<EmployeeSettings>>> UpdateEmployeeSettings(UpdateEmployeeSettingsCommand command)
    {
        var result = await Sender.Send(command);

        return result;
    }
}
