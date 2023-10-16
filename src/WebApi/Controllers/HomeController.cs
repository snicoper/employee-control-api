using EmployeeControl.Application.Common.Interfaces;
using EmployeeControl.Application.Common.Models.Emails;
using EmployeeControl.Application.Features.Home.Queries.Prueba;
using EmployeeControl.WebApi.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeControl.WebApi.Controllers;

[Route("api/v{version:apiVersion}/home")]
public class HomeController(IEmailService emailService, IRazorViewToStringRendererService razorViewToStringRendererService)
    : ApiControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PruebaResponse>> Prueba()
    {
        return await Mediator.Send(new PruebaQuery());
    }

    [HttpGet("test-email")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<string>> TestEmail()
    {
        emailService.To.Add("snicoper@example.com");
        emailService.Subject = "Mensaje de prueba";
        var model = new TestEmailViewModel { Name = "Salvador Nicolas" };

        await emailService.SendMailWithViewAsync("TestEmail", model);

        return "Ok";
    }

    [HttpGet("test-render-html")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<string>> TestRenderHtml()
    {
        return await razorViewToStringRendererService.RenderViewToStringAsync(
            "TestRenderToString",
            new TestEmailViewModel { Name = "Test" },
            new Dictionary<string, object?>());
    }
}
