using EmployeeControl.Application.Common.Models;
using EmployeeControl.Application.Cqrs.Admin.AdminIdentity.Queries;
using EmployeeControl.WebApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeControl.WebApi.Controllers.Admin.Identity;

[Route("api/v{version:apiVersion}/admin/identity")]
public class AdminIdentityController : ApiControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ResponseData<GetAdminIdentitiesDto>>> GetAdminIdentities([FromQuery] RequestData request)
    {
        return await Mediator.Send(new GetAdminIdentitiesQuery(request));
    }
}
