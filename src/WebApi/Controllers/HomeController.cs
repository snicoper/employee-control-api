using EmployeeControl.Application.Common.Interfaces;
using EmployeeControl.Application.Common.Models.Emails;
using EmployeeControl.Application.Cqrs.Home.Queries.Prueba;
using EmployeeControl.WebApi.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeControl.WebApi.Controllers;

[Route("api/v{version:apiVersion}/home")]
public class HomeController : ApiControllerBase
{
    private readonly IEmailService _emailService;

    public HomeController(IEmailService emailService)
    {
        _emailService = emailService;
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PruebaDto>> Prueba()
    {
        return await Mediator.Send(new PruebaQuery());
    }

    [HttpGet("mail")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<string>> SendMailTest()
    {
        _emailService.To.Add("snicoper@outlook.com");
        _emailService.Subject = "Mensaje de prueba";
        var model = new TestEmailDto { Name = "Salvador Nicolas" };

        await _emailService.SendMailWithViewAsync("TestEmail", model);

        return "Ok";
    }
}
