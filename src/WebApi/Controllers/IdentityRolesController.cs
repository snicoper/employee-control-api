using EmployeeControl.Application.Features.IdentityRoles.Queries.GetAllIdentityRoles;
using EmployeeControl.WebApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeControl.WebApi.Controllers;

[Route("api/v{version:apiVersion}/identity-roles")]
public class IdentityRolesController : ApiControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ICollection<GetAllIdentityRolesResponse>>> GetAllIdentityRoles()
    {
        var result = await Sender.Send(new GetAllIdentityRolesQuery());

        return result.ToList();
    }
}
