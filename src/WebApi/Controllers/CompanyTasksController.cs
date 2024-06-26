﻿using EmployeeControl.Application.Common.Models;
using EmployeeControl.Application.Features.CompanyTasks.Commands.ActivateCompanyTask;
using EmployeeControl.Application.Features.CompanyTasks.Commands.AssignEmployeesToTask;
using EmployeeControl.Application.Features.CompanyTasks.Commands.CreateCompanyTask;
using EmployeeControl.Application.Features.CompanyTasks.Commands.DeactivateCompanyTask;
using EmployeeControl.Application.Features.CompanyTasks.Commands.UpdateCompanyTask;
using EmployeeControl.Application.Features.CompanyTasks.Queries.GetCompanyTasksByEmployeeIdPaginated;
using EmployeeControl.Application.Features.CompanyTasks.Queries.GetCompanyTasksById;
using EmployeeControl.Application.Features.CompanyTasks.Queries.GetCompanyTasksPaginated;
using EmployeeControl.Application.Features.CompanyTasks.Queries.GetEmployeesByCompanyTaskIdPaginated;
using EmployeeControl.Application.Features.CompanyTasks.Queries.GetEmployeesUnassignedTaskByCompanyTaskId;
using EmployeeControl.Domain.Common;
using EmployeeControl.WebApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeControl.WebApi.Controllers;

[Route("api/v{version:apiVersion}/tasks")]
public class CompanyTasksController : ApiControllerBase
{
    /// <summary>
    /// Obtener lista paginada de tareas.
    /// </summary>
    /// <param name="request">RequestData.</param>
    /// <returns>Lista de tareas paginádas.</returns>
    [HttpGet("paginated")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<ResponseData<GetCompanyTasksPaginatedResponse>>>>
        GetCompanyTasksPaginated([FromQuery] RequestData request)
    {
        var result = await Sender.Send(new GetCompanyTasksPaginatedQuery(request));

        return result;
    }

    /// <summary>
    /// Obtener lista paginada de empleados por el Id de la tarea.
    /// </summary>
    /// <param name="request">RequestData.</param>
    /// <param name="id">Id tarea.</param>
    /// <returns>Lista de usuarios paginádos.</returns>
    [HttpGet("{id:guid}/employees/paginated")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<ResponseData<GetEmployeesByCompanyTaskIdPaginatedResponse>>>>
        GetEmployeesByCompanyTaskIdPaginated([FromQuery] RequestData request, Guid id)
    {
        var result = await Sender.Send(new GetEmployeesByCompanyTaskIdPaginatedQuery(request, id));

        return result;
    }

    /// <summary>
    /// Obtener lista paginada de tareas por el Id del empleado.
    /// </summary>
    /// <param name="request">RequestData.</param>
    /// <param name="employeeId">Id empleado.</param>
    /// <returns>Lista de tareas paginádas.</returns>
    [HttpGet("employees/{employeeId:guid}/paginated")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<ResponseData<GetCompanyTasksByEmployeeIdPaginatedResponse>>>>
        GetCompanyTasksByEmployeeIdPaginated([FromQuery] RequestData request, Guid employeeId)
    {
        var result = await Sender.Send(new GetCompanyTasksByEmployeeIdPaginatedQuery(request, employeeId));

        return result;
    }

    /// <summary>
    /// Obtener lista de todos los empleados que no tengan asignada una tarea concreta.
    /// </summary>
    /// <param name="id">Id tarea.</param>
    /// <returns>Lista empleados que no pertenecen a una tarea concreta.</returns>
    [HttpGet("{id:guid}/employees/unassigned")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<Result<List<GetEmployeesUnassignedTaskByCompanyTaskIdResponse>>> GetEmployeesUnassignedTaskByCompanyTaskId(
        Guid id)
    {
        var result = await Sender.Send(new GetEmployeesUnassignedTaskByCompanyTaskIdQuery(id));

        return result;
    }

    /// <summary>
    /// Obtener una tarea por su Id.
    /// </summary>
    /// <param name="id">Id de la tarea.</param>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<GetCompanyTasksByIdResponse>>> GetCompanyTasksById(Guid id)
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
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<string>>> CreateCompanyTask(CreateCompanyTaskCommand command)
    {
        var result = await Sender.Send(command);

        return ResultWithStatus(result, StatusCodes.Status201Created);
    }

    /// <summary>
    /// Asignar empleados a una tarea concreta.
    /// </summary>
    /// <param name="command">Lista de Ids de empleado a asignar y la Id de la tarea.</param>
    [HttpPut("{id:guid}/employees/assign")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result>> AssignEmployeesToTask(AssignEmployeesToTaskCommand command)
    {
        var result = await Sender.Send(command);

        return result;
    }

    /// <summary>
    /// Actualizar una tarea.
    /// </summary>
    /// <param name="command">Datos a actualizar de la tarea.</param>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
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
    [HttpPut("{id:guid}/activate")]
    [ProducesResponseType(StatusCodes.Status200OK)]
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
    [HttpPut("{id:guid}/deactivate")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result>> DeactivateCompanyTask(DeactivateCompanyTaskCommand command)
    {
        var result = await Sender.Send(command);

        return result;
    }
}
