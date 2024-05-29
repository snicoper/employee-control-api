using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl.Application.Features.Departments.Queries.GetEmployeesByDepartmentIdPaginated;

internal class GetEmployeesByDepartmentIdPaginatedHandler(IApplicationDbContext context, IMapper mapper)
    : IQueryHandler<GetEmployeesByDepartmentIdPaginatedQuery, ResponseData<GetEmployeesByDepartmentIdPaginatedResponse>>
{
    public async Task<Result<ResponseData<GetEmployeesByDepartmentIdPaginatedResponse>>> Handle(
        GetEmployeesByDepartmentIdPaginatedQuery request,
        CancellationToken cancellationToken)
    {
        var users = context
            .EmployeeDepartments
            .Include(uct => uct.User)
            .Where(uct => uct.DepartmentId == request.DepartmentId)
            .Select(uct => uct.User);

        var resultResponse = await ResponseData<GetEmployeesByDepartmentIdPaginatedResponse>.CreateAsync(
            users,
            request.RequestData,
            mapper,
            cancellationToken);

        return Result.Success(resultResponse);
    }
}
