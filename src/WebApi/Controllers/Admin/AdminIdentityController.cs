﻿using EmployeeControl.Application.Common.Models;
using EmployeeControl.Application.Cqrs.Admin.AdminIdentity.Queries.GetAdminIdentitiesPaginated;
using EmployeeControl.WebApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeControl.WebApi.Controllers.Admin;

[Route("api/v{version:apiVersion}/admin/identity")]
public class AdminIdentityController : ApiControllerBase
{
    /// <summary>
    ///     Obtener lista de usuarios paginados.
    /// </summary>
    /// <param name="request">Datos de paginación.</param>
    /// <returns>Lista de usuarios paginados.</returns>
    [HttpGet("paginated")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ResponseData<GetAdminIdentitiesPaginatedDto>>> GetAdminIdentitiesPaginated(
        [FromQuery] RequestData request)
    {
        return await Mediator.Send(new GetAdminIdentitiesPaginatedQuery(request));
    }
}