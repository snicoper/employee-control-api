using EmployeeControl.Application.Cqrs.Home.Queries.Prueba;
using EmployeeControl.WebApi.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeControl.WebApi.Controllers;

[Authorize]
[Route("api/v{version:apiVersion}/home")]
public class HomeController : ApiControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PruebaDto>> Prueba()
    {
        return await Mediator.Send(new PruebaQuery());
    }
}
