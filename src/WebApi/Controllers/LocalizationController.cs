using EmployeeControl.Application.Features.Localization.Queries.CurrentLocale;
using EmployeeControl.Application.Features.Localization.Queries.SupportedLocales;
using EmployeeControl.WebApi.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeControl.WebApi.Controllers;

[Route("api/v{version:apiVersion}/localization")]
public class LocalizationController : ApiControllerBase
{
    /// <summary>
    /// Obtener cultura actual.
    /// </summary>
    /// <returns>Cultura actual.</returns>
    [AllowAnonymous]
    [HttpGet("current-locale")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<CurrentLocaleResponse>> CurrentLocale()
    {
        return await Sender.Send(new CurrentLocaleQuery());
    }

    /// <summary>
    /// Obtener culturas soportadas.
    /// </summary>
    /// <returns>Lista de culturas soportadas.</returns>
    [AllowAnonymous]
    [HttpGet("supported-locales")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<SupportedLocalesResponse>> SupportedLocales()
    {
        return await Sender.Send(new SupportedLocalesQuery());
    }
}
