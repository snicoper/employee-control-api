﻿using EmployeeControl.Application.Common.Models;
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
}
