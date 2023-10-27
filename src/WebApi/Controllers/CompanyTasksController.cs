using EmployeeControl.Application.Common.Models;
using EmployeeControl.Application.Features.CompanyTasks.Commands.CreateCompanyTask;
using EmployeeControl.Application.Features.CompanyTasks.Queries.GetCompanyTasksById;
using EmployeeControl.Application.Features.CompanyTasks.Queries.GetCompanyTasksByIdAndCompanyId;
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
    /// Obtener una tarea por su Id.
    /// </summary>
    /// <param name="id">Id de la tarea.</param>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetCompanyTasksByIdResponse>> GetCompanyTasksById(int id)
    {
        var result = await Sender.Send(new GetCompanyTasksByIdQuery(id));

        return result;
    }

    /// <summary>
    /// Obtener una tarea por su Id.
    /// <para>Solo se obtiene la tarea si es de la misma compañía.</para>
    /// </summary>
    /// <param name="id">Id de la tarea.</param>
    /// <param name="companyId">Id compañía.</param>
    [HttpGet("{id}/companies/{companyId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetCompanyTasksByIdAndCompanyIdResponse>> GetCompanyTasksByIdAndCompanyId(
        int id,
        int companyId)
    {
        var result = await Sender.Send(new GetCompanyTasksByIdAndCompanyIdQuery(id, companyId));

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
}
