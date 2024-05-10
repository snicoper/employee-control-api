﻿using EmployeeControl.Application.Common.Models;
using EmployeeControl.Application.Features.CompanyCalendarHolidays.Command.CreateCompanyCalendarHoliday;
using EmployeeControl.Application.Features.CompanyCalendarHolidays.Command.DeleteCompanyCalendarHoliday;
using EmployeeControl.Application.Features.CompanyCalendarHolidays.Command.UpdateCompanyCalendarHoliday;
using EmployeeControl.Application.Features.CompanyCalendarHolidays.Queries.GetCompanyCalendarHolidaysByCompanyCalendarIdAndYear;
using EmployeeControl.Domain.Entities;
using EmployeeControl.WebApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeControl.WebApi.Controllers;

[Route("api/v{version:apiVersion}/company-calendar-holidays")]
public class CompanyCalendarHolidayController : ApiControllerBase
{
    /// <summary>
    /// Obtener lista de <see cref="CompanyCalendarHoliday" /> filtrado por año e Id de <see cref="CompanyCalendar" />.
    /// </summary>
    /// <param name="companyCalendarId">Id <see cref="CompanyCalendar" /> a filtrar.</param>
    /// <param name="year">Año al que obtener <see cref="CompanyCalendarHoliday" />.</param>
    /// <returns>Lista de departamentos paginádos.</returns>
    [HttpGet("company-calendars/{companyCalendarId}/year/{year:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<ICollection<GetCompanyCalendarHolidaysByCompanyCalendarIdAndYearResponse>>>>
        GetCompanyCalendarHolidaysByCompanyCalendarIdAndYear(string companyCalendarId, int year)
    {
        var result = await Sender.Send(new GetCompanyCalendarHolidaysByCompanyCalendarIdAndYearQuery(companyCalendarId, year));

        return result;
    }

    /// <summary>
    /// Crear un nuevo <see cref="CompanyCalendarHoliday" />.
    /// </summary>
    /// <param name="command">Datos del <see cref="CompanyCalendarHoliday" />.</param>
    /// <returns>Id del <see cref="CompanyCalendarHoliday" />. creado en caso de éxito.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<string>>> CreateCompanyCalendarHoliday(CreateCompanyCalendarHolidayCommand command)
    {
        var result = await Sender.Send(command);

        return ResultWithStatus(result, StatusCodes.Status201Created);
    }

    /// <summary>
    /// Actualiza un <see cref="CompanyCalendarHoliday" />.
    /// </summary>
    /// <param name="command">Datos del <see cref="CompanyCalendarHoliday" />.</param>
    /// <returns><see cref="Result" />.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result>> UpdateCompanyCalendarHoliday(UpdateCompanyCalendarHolidayCommand command)
    {
        var result = await Sender.Send(command);

        return result;
    }

    /// <summary>
    /// Elimina un <see cref="CompanyCalendarHoliday" />.
    /// </summary>
    /// <param name="id">Id <see cref="CompanyCalendarHoliday" /> a eliminar.</param>
    /// <returns><see cref="Result" />.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result>> DeleteCompanyCalendarHoliday(string id)
    {
        var result = await Sender.Send(new DeleteCompanyCalendarHolidayCommand(id));

        return result;
    }
}
