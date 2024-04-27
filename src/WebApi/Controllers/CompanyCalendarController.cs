using EmployeeControl.Application.Features.CompanyCalendars.Queries.GetCompanyCalendarById;
using EmployeeControl.Application.Features.CompanyCalendars.Queries.GetCompanyCalendars;
using EmployeeControl.Domain.Entities;
using EmployeeControl.WebApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeControl.WebApi.Controllers;

[Route("api/v{version:apiVersion}/company-calendars")]
public class CompanyCalendarController : ApiControllerBase
{
    /// <summary>
    /// Obtener lista de <see cref="CompanyCalendar" />.
    /// </summary>
    /// <returns>Lista de <see cref="CompanyCalendar" />.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ICollection<GetCompanyCalendarsResponse>>> GetCompanyCalendars()
    {
        var result = await Sender.Send(new GetCompanyCalendarsQuery());

        return result.ToList();
    }

    /// <summary>
    /// Obtener un <see cref="CompanyCalendar" /> por su Id.
    /// </summary>
    /// <returns><see cref="CompanyCalendar" />.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetCompanyCalendarByIdResponse>> GetCompanyCalendarById(string id)
    {
        var result = await Sender.Send(new GetCompanyCalendarByIdQuery(id));

        return result;
    }
}
