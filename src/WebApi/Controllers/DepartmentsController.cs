using EmployeeControl.Application.Features.Departments.Commands.CreateDepartment;
using EmployeeControl.Domain.Entities;
using EmployeeControl.WebApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeControl.WebApi.Controllers;

[Route("api/v{version:apiVersion}/departments")]
public class DepartmentsController : ApiControllerBase
{
    /// <summary>
    /// Crear nuevo <see cref="Department" />.
    /// </summary>
    /// <param name="command">Datos del departamento.</param>
    /// <returns>Id del departamento creado en caso de éxito.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreateDepartmentResponse>> CreateDepartment(CreateDepartmentCommand command)
    {
        var result = await Sender.Send(command);

        return ObjectResultWithStatusCode(result, StatusCodes.Status201Created);
    }
}
