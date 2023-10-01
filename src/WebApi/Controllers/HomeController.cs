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
    private readonly IRazorViewToStringRendererService _razorViewToStringRendererService;

    public HomeController(IEmailService emailService, IRazorViewToStringRendererService razorViewToStringRendererService)
    {
        _emailService = emailService;
        _razorViewToStringRendererService = razorViewToStringRendererService;
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PruebaDto>> Prueba()
    {
        return await Mediator.Send(new PruebaQuery());
    }

    [HttpGet("test-email")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<string>> TestEmail()
    {
        _emailService.To.Add("snicoper@example.com");
        _emailService.Subject = "Mensaje de prueba";
        var model = new TestEmailViewModel { Name = "Salvador Nicolas" };

        await _emailService.SendMailWithViewAsync("TestEmail", model);

        return "Ok";
    }

    [HttpGet("test-render-html")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<string>> TestRenderHtml()
    {
        return await _razorViewToStringRendererService.RenderViewToStringAsync(
            "TestRenderToString",
            new TestEmailViewModel { Name = "Test" },
            new Dictionary<string, object?>());
    }
}
