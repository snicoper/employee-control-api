using EmployeeControl.Application.Common.Models;
using EmployeeControl.Application.Features.Departments.Commands.ActivateDepartment;
using EmployeeControl.Application.Features.Departments.Commands.CreateDepartment;
using EmployeeControl.Application.Features.Departments.Commands.DeactivateDepartment;
using EmployeeControl.Application.Features.Departments.Queries.GetDepartmentById;
using EmployeeControl.Application.Features.Departments.Queries.GetDepartmentsByCompanyIdPaginated;
using EmployeeControl.Domain.Entities;
using EmployeeControl.WebApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeControl.WebApi.Controllers;

[Route("api/v{version:apiVersion}/departments")]
public class DepartmentsController : ApiControllerBase
{
    /// <summary>
    /// Obtener lista de departamentos paginados.
    /// </summary>
    /// <param name="companyId">Id compañía.</param>
    /// <param name="request">RequestData.</param>
    /// <returns>Lista de departamentos paginádos.</returns>
    [HttpGet("companies/{companyId}/paginated")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ResponseData<GetDepartmentsByCompanyIdPaginatedResponse>>> GetDepartmentsByCompanyIdPaginated(
        string companyId,
        [FromQuery] RequestData request)
    {
        var result = await Sender.Send(new GetDepartmentsByCompanyIdPaginatedQuery(companyId, request));

        return result;
    }

    /// <summary>
    /// Obtener un <see cref="Department" /> por su Id.
    /// </summary>
    /// <param name="id">Id del departamento.</param>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetDepartmentByIdResponse>> GetDepartmentById(string id)
    {
        var result = await Sender.Send(new GetDepartmentByIdQuery(id));

        return result;
    }

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

    /// <summary>
    /// Activa un <see cref="Department" />.
    /// </summary>
    /// <param name="command">Id del <see cref="Department" />.</param>
    /// <returns><see cref="Result" />.</returns>
    [HttpPut("activate")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result>> ActivateDepartment(ActivateDepartmentCommand command)
    {
        var result = await Sender.Send(command);

        return result;
    }

    /// <summary>
    /// Desactiva un <see cref="Department" />.
    /// </summary>
    /// <param name="command">Id del <see cref="Department" />.</param>
    /// <returns><see cref="Result" />.</returns>
    [HttpPut("deactivate")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result>> DeactivateDepartment(DeactivateDepartmentCommand command)
    {
        var result = await Sender.Send(command);

        return result;
    }
}
