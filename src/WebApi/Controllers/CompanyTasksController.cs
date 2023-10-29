﻿using EmployeeControl.Application.Common.Models;
using EmployeeControl.Application.Features.CompanyTasks.Commands.ActivateCompanyTask;
using EmployeeControl.Application.Features.CompanyTasks.Commands.CreateCompanyTask;
using EmployeeControl.Application.Features.CompanyTasks.Commands.DeactivateCompanyTask;
using EmployeeControl.Application.Features.CompanyTasks.Commands.UpdateCompanyTask;
using EmployeeControl.Application.Features.CompanyTasks.Queries.GetCompanyTasksByCompanyIdPaginated;
using EmployeeControl.Application.Features.CompanyTasks.Queries.GetCompanyTasksByEmployeeIdPaginated;
using EmployeeControl.Application.Features.CompanyTasks.Queries.GetCompanyTasksById;
using EmployeeControl.Application.Features.CompanyTasks.Queries.GetUsersByCompanyTaskIdPaginated;
using EmployeeControl.WebApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeControl.WebApi.Controllers;

[Route("api/v{version:apiVersion}/tasks")]
public class CompanyTasksController : ApiControllerBase
{
    /// <summary>
    /// Obtener lista paginada de tareas por el Id de la compañía.
    /// </summary>
    /// <param name="request">RequestData.</param>
    /// <param name="companyId">Id compañía.</param>
    /// <returns>Lista de tareas de la compañía paginádos.</returns>
    [HttpGet("companies/{companyId}/paginated")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ResponseData<GetCompanyTasksByCompanyIdPaginatedResponse>>>
        GetCompanyTasksByCompanyIdPaginated([FromQuery] RequestData request, string companyId)
    {
        var result = await Sender.Send(new GetCompanyTasksByCompanyIdPaginatedQuery(request, companyId));

        return result;
    }

    /// <summary>
    /// Obtener lista paginada de empleados por el Id de la tarea.
    /// </summary>
    /// <param name="request">RequestData.</param>
    /// <param name="id">Id tarea.</param>
    /// <returns>Lista de usuarios paginádos.</returns>
    [HttpGet("{id}/employees/paginated")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ResponseData<GetUsersByCompanyTaskIdPaginatedResponse>>>
        GetUsersByCompanyTaskIdPaginated([FromQuery] RequestData request, string id)
    {
        var result = await Sender.Send(new GetUsersByCompanyTaskIdPaginatedQuery(request, id));

        return result;
    }

    /// <summary>
    /// Obtener lista paginada de tareas por el Id del empleado.
    /// </summary>
    /// <param name="request">RequestData.</param>
    /// <param name="employeeId">Id empleado.</param>
    /// <returns>Lista de tareas paginádas.</returns>
    [HttpGet("employees/{employeeId}/paginated")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ResponseData<GetCompanyTasksByEmployeeIdPaginatedResponse>>>
        GetCompanyTasksByEmployeeIdPaginated([FromQuery] RequestData request, string employeeId)
    {
        var result = await Sender.Send(new GetCompanyTasksByEmployeeIdPaginatedQuery(request, employeeId));

        return result;
    }

    /// <summary>
    /// Obtener una tarea por su Id.
    /// </summary>
    /// <param name="id">Id de la tarea.</param>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetCompanyTasksByIdResponse>> GetCompanyTasksById(string id)
    {
        var result = await Sender.Send(new GetCompanyTasksByIdQuery(id));

        return result;
    }

    /// <summary>
    /// Crear una nueva tarea.
    /// </summary>
    /// <param name="command">Datos para la creación de la tarea.</param>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<int>> CreateCompanyTask(CreateCompanyTaskCommand command)
    {
        var result = await Sender.Send(command);

        return ObjectResultWithStatusCode(result, StatusCodes.Status201Created);
    }

    /// <summary>
    /// Actualizar una tarea.
    /// </summary>
    /// <param name="command">Datos a actualizar de la tarea.</param>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result>> UpdateCompanyTask(UpdateCompanyTaskCommand command)
    {
        var result = await Sender.Send(command);

        return result;
    }

    /// <summary>
    /// Activar una tarea de compañía.
    /// </summary>
    /// <param name="command">Id tarea a activar.</param>
    [HttpPut("{id}/activate")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result>> ActivateCompanyTask(ActivateCompanyTaskCommand command)
    {
        var result = await Sender.Send(command);

        return result;
    }

    /// <summary>
    /// Desactivar una tarea de compañía.
    /// </summary>
    /// <param name="command">Id tarea a activar.</param>
    [HttpPut("{id}/deactivate")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result>> DeactivateCompanyTask(DeactivateCompanyTaskCommand command)
    {
        var result = await Sender.Send(command);

        return result;
    }
}
