using EmployeeControl.Application.Features.Culture.Queries.CurrentCulture;
using EmployeeControl.Application.Features.Culture.Queries.SupportedCultures;
using EmployeeControl.WebApi.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeControl.WebApi.Controllers;

[Route("api/v{version:apiVersion}/culture")]
public class CultureController : ApiControllerBase
{
    /// <summary>
    /// Obtener cultura actual.
    /// </summary>
    /// <returns>Cultura actual.</returns>
    [AllowAnonymous]
    [HttpGet("current")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<CurrentCultureDto>> CurrentCulture()
    {
        return await Mediator.Send(new CurrentCultureQuery());
    }

    /// <summary>
    /// Obtener culturas soportadas.
    /// </summary>
    /// <returns>Lista de culturas soportadas.</returns>
    [AllowAnonymous]
    [HttpGet("supported")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<SupportedCulturesDto>> SupportedCultures()
    {
        return await Mediator.Send(new SupportedCulturesQuery());
    }
}
