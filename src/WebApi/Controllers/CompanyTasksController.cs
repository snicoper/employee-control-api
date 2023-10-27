﻿using EmployeeControl.Application.Common.Models;
using EmployeeControl.Application.Features.CompanyTasks.Commands.CreateCompanyTask;
using EmployeeControl.Application.Features.CompanyTasks.Queries.GetCompanyTasksPaginated;
using EmployeeControl.WebApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeControl.WebApi.Controllers;

[Route("api/v{version:apiVersion}/tasks")]
public class CompanyTasksController : ApiControllerBase
{
    /// <summary>
    /// Obtener lista de tareas de una compañía concreta.
    /// </summary>
    /// <param name="request">RequestData.</param>
    /// <param name="companyId">Id compañía.</param>
    /// <returns>Lista de tareas de la compañía paginádos.</returns>
    [HttpGet("company/{companyId}/paginated")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ResponseData<GetCompanyTasksPaginatedByCompanyIdResponse>>>
        GetCompanyTasksPaginatedByCompanyId([FromQuery] RequestData request, int companyId)
    {
        var result = await Sender.Send(new GetCompanyTasksPaginatedByCompanyIdQuery(request, companyId));

        return result;
    }

    /// <summary>
    /// Crear una nueva tarea.
    /// </summary>
    /// <param name="command">Datos para la creación de la tarea.</param>
    /// <returns>Result.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<int>> CreateCompanyTask(CreateCompanyTaskCommand command)
    {
        var result = await Sender.Send(command);

        return ObjectResultWithStatusCode(result, StatusCodes.Status201Created);
    }
}
