﻿using EmployeeControl.Application.Common.Models;
using EmployeeControl.Application.Features.CategoryAbsences.Commands.CreateCategoryAbsence;
using EmployeeControl.Application.Features.CategoryAbsences.Commands.UpdateCategoryAbsence;
using EmployeeControl.Application.Features.CategoryAbsences.Queries.GetCategoryAbsenceById;
using EmployeeControl.Application.Features.CategoryAbsences.Queries.GetCategoryAbsencePaginated;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Entities;
using EmployeeControl.WebApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeControl.WebApi.Controllers;

[Route("api/v{version:apiVersion}/category-absence")]
public class CategoryAbsenceController : ApiControllerBase
{
    /// <summary>
    /// Obtener lista paginada de categorías de ausencias.
    /// </summary>
    /// <param name="request">RequestData.</param>
    /// <returns>Lista de las categorías de ausencias de la compañía paginádos.</returns>
    [HttpGet("paginated")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<ResponseData<GetCategoryAbsencePaginatedResponse>>>>
        GetCategoryAbsencePaginated([FromQuery] RequestData request)
    {
        var result = await Sender.Send(new GetCategoryAbsencePaginatedQuery(request));

        return result;
    }

    /// <summary>
    /// Obtener una <see cref="CategoryAbsence" /> por su Id.
    /// </summary>
    /// <param name="id">Id de la <see cref="CategoryAbsence" />.</param>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<GetCategoryAbsenceByIdResponse>>> GetCategoryAbsenceById(string id)
    {
        var result = await Sender.Send(new GetCategoryAbsenceByIdQuery(id));

        return result;
    }

    /// <summary>
    /// Crear un nuevo <see cref="CategoryAbsence" />.
    /// </summary>
    /// <param name="command">Datos del <see cref="CategoryAbsence" />.</param>
    /// <returns>Id del <see cref="CategoryAbsence" />. creado en caso de éxito.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<string>>> CreateCompanyAbsence(CreateCategoryAbsenceCommand command)
    {
        var result = await Sender.Send(command);

        return ResultWithStatus(result, StatusCodes.Status201Created);
    }

    /// <summary>
    /// Actualizar una <see cref="CategoryAbsence" />.
    /// </summary>
    /// <param name="command">Datos a actualizar de la <see cref="CategoryAbsence" />.</param>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result>> UpdateCategoryAbsence(UpdateCategoryAbsenceCommand command)
    {
        var result = await Sender.Send(command);

        return result;
    }
}
