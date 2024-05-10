using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Features.Departments;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;

namespace EmployeeControl.Application.Features.Departments.Queries.GetDepartmentsByEmployeeIdPaginated;

public class GetDepartmentsByEmployeeIdPaginatedHandler(IDepartmentService departmentService, IMapper mapper)
    : IQueryHandler<GetDepartmentsByEmployeeIdPaginatedQuery, ResponseData<GetDepartmentsByEmployeeIdPaginatedResponse>>
{
    public async Task<Result<ResponseData<GetDepartmentsByEmployeeIdPaginatedResponse>>> Handle(
        GetDepartmentsByEmployeeIdPaginatedQuery request,
        CancellationToken cancellationToken)
    {
        var departments = departmentService.GetAllByEmployeeIdQueryable(request.EmployeeId);

        var resultResponse = await ResponseData<GetDepartmentsByEmployeeIdPaginatedResponse>.CreateAsync(
            departments,
            request.RequestData,
            mapper,
            cancellationToken);

        return Result.Success(resultResponse);
    }
}
