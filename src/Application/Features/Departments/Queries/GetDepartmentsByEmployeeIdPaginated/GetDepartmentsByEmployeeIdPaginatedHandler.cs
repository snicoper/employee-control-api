using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Repositories;

namespace EmployeeControl.Application.Features.Departments.Queries.GetDepartmentsByEmployeeIdPaginated;

public class GetDepartmentsByEmployeeIdPaginatedHandler(IDepartmentRepository departmentRepository, IMapper mapper)
    : IQueryHandler<GetDepartmentsByEmployeeIdPaginatedQuery, ResponseData<GetDepartmentsByEmployeeIdPaginatedResponse>>
{
    public async Task<Result<ResponseData<GetDepartmentsByEmployeeIdPaginatedResponse>>> Handle(
        GetDepartmentsByEmployeeIdPaginatedQuery request,
        CancellationToken cancellationToken)
    {
        var departments = departmentRepository.GetAllByEmployeeIdQueryable(request.EmployeeId);

        var resultResponse = await ResponseData<GetDepartmentsByEmployeeIdPaginatedResponse>.CreateAsync(
            departments,
            request.RequestData,
            mapper,
            cancellationToken);

        return Result.Success(resultResponse);
    }
}
