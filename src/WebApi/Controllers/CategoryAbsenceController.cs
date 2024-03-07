using EmployeeControl.Application.Common.Models;
using EmployeeControl.Application.Features.CategoryAbsences.Commands.CreateCategoryAbsence;
using EmployeeControl.Domain.Entities;
using EmployeeControl.WebApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeControl.WebApi.Controllers;

[Route("api/v{version:apiVersion}/category-absence")]
public class CategoryAbsenceController : ApiControllerBase
{
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
