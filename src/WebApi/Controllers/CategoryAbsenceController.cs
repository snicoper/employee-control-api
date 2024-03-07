using EmployeeControl.Application.Common.Models;
using EmployeeControl.Application.Features.CategoryAbsences.Commands.CreateCategoryAbsence;
using EmployeeControl.Application.Features.CategoryAbsences.Queries.GetCategoryAbsenceByCompanyIdPaginated;
using EmployeeControl.Domain.Entities;
using EmployeeControl.WebApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeControl.WebApi.Controllers;

[Route("api/v{version:apiVersion}/category-absence")]
public class CategoryAbsenceController : ApiControllerBase
{
    /// <summary>
    /// Obtener lista paginada de categorías de ausencias por el Id de la compañía.
    /// </summary>
    /// <param name="request">RequestData.</param>
    /// <param name="companyId">Id compañía.</param>
    /// <returns>Lista de las categorías de ausencias de la compañía paginádos.</returns>
    [HttpGet("companies/{companyId}/paginated")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ResponseData<GetCategoryAbsenceByCompanyIdPaginatedResponse>>>
        GetCategoryAbsenceByCompanyIdPaginated([FromQuery] RequestData request, string companyId)
    {
        var result = await Sender.Send(new GetCategoryAbsenceByCompanyIdPaginatedQuery(request, companyId));

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
    public async Task<ActionResult<Result>> CreateCompanyAbsence(CreateCategoryAbsenceCommand command)
    {
        var result = await Sender.Send(command);

        return ObjectResultWithStatusCode(result.Id, StatusCodes.Status201Created);
    }
}
