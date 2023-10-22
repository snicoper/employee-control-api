﻿using EmployeeControl.Application.Features.Localizations.Queries.CurrentLocale;
using EmployeeControl.Application.Features.Localizations.Queries.SupportedLocales;
using EmployeeControl.WebApi.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeControl.WebApi.Controllers;

[Route("api/v{version:apiVersion}/localizations")]
public class LocalizationsController : ApiControllerBase
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
        var result = await Sender.Send(new CurrentLocaleQuery());

        return result;
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
        var result = await Sender.Send(new SupportedLocalesQuery());

        return result;
    }
}
