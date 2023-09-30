using EmployeeControl.Application.Common.Constants;
using EmployeeControl.Application.Common.Extensions;
using EmployeeControl.WebApi.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeControl.WebApi.Controllers;

[Route("api/v{version:apiVersion}/culture")]
public class CultureController : ApiControllerBase
{
    /// <summary>
    ///     Obtener cultura actual.
    /// </summary>
    /// <returns>Cultura actual.</returns>
    [HttpGet("current")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<string> CurrentCulture()
    {
        var culture = Request.HttpContext.Features.Get<IRequestCultureFeature>();
        var current = culture?.RequestCulture.Culture.ToString();

        return current.NotNull();
    }

    /// <summary>
    ///     Obtener culturas soportadas..
    /// </summary>
    /// <returns>Lista de culturas soportadas.</returns>
    [HttpGet("supported")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<ICollection<string>> GetSupportedCultures()
    {
        return AppCultures.GetAll().Select(c => c.Name).ToArray();
    }
}
