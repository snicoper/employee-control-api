﻿using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Entities;
using EmployeeControl.Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl.Application.Features.Departments.Queries.GetEmployeesUnassignedDepartmentByDepartmentId;

internal class GetEmployeesUnassignedDepartmentByDepartmentIdHandler(
    IApplicationDbContext context,
    UserManager<User> userManager)
    : IQueryHandler<
        GetEmployeesUnassignedDepartmentByDepartmentIdQuery,
        List<GetEmployeesUnassignedDepartmentByDepartmentIdResponse>>
{
    public async Task<Result<List<GetEmployeesUnassignedDepartmentByDepartmentIdResponse>>> Handle(
        GetEmployeesUnassignedDepartmentByDepartmentIdQuery request,
        CancellationToken cancellationToken)
    {
        // Obtener el departamento por su Id.
        var department = await context
                             .Departments
                             .Include(ct => ct.EmployeeDepartments)
                             .ThenInclude(uct => uct.User)
                             .SingleOrDefaultAsync(ct => ct.Id == request.Id, cancellationToken) ??
                         throw new NotFoundException(nameof(Department), nameof(Department.Id));

        // Filtrar los Ids de los empleados que ya tienen asignado el departamento.
        var userIdsInDepartment = department
            .EmployeeDepartments
            .Select(uct => uct.User)
            .Select(au => au!.Id)
            .ToList();

        // Obtener los empleados de la empresa excluyendo los empleados que ya tienen el departamento asignada.
        var users = userManager
            .Users
            .Include(
                au => au
                    .EmployeeCompanyTasks
                    .Where(uct => uct.CompanyTaskId == request.Id && uct.CompanyTaskId == department.Id))
            .Where(au => !userIdsInDepartment.Contains(au.Id));

        if (!users.Any())
        {
            return Result.Success(new List<GetEmployeesUnassignedDepartmentByDepartmentIdResponse>());
        }

        // Preparar la respuesta.
        var resultResponse = users
            .Select(
                uct => new GetEmployeesUnassignedDepartmentByDepartmentIdResponse(
                    uct!.Id,
                    $"{uct.FirstName} {uct.LastName} <{uct.Email}>"))
            .ToList();

        return Result.Success(resultResponse);
    }
}
