using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl.Application.Features.EmployeeHolidays.Queries.GetEmployeeHolidaysByYearPaginated;

internal class GetEmployeeHolidaysByYearPaginatedHandler(
    UserManager<User> userManager,
    IMapper mapper)
    : IQueryHandler<GetEmployeeHolidaysByYearPaginatedQuery, ResponseData<GetEmployeeHolidaysByYearPaginatedResponse>>
{
    public async Task<Result<ResponseData<GetEmployeeHolidaysByYearPaginatedResponse>>> Handle(
        GetEmployeeHolidaysByYearPaginatedQuery request,
        CancellationToken cancellationToken)
    {
        // Obtener empleados con el EmployeeHoliday en base a request.Year.
        // Un empleado solo puede tener un EmployeeHoliday del mismo año.
        var employees = userManager
            .Users
            .Include(au => au.EmployeeHolidays.Where(eh => eh.Year == request.Year))
            .Where(au => au.Active);

        // El mapeo se hace como si fuese una entidad EmployeeHoliday en vez de User.
        var resultResponse = await ResponseData<GetEmployeeHolidaysByYearPaginatedResponse>.CreateAsync(
            employees,
            request.RequestData,
            mapper,
            cancellationToken);

        return Result.Success(resultResponse);
    }
}
