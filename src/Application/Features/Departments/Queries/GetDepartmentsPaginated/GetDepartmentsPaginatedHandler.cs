using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Features.Departments;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;

namespace EmployeeControl.Application.Features.Departments.Queries.GetDepartmentsPaginated;

internal class GetDepartmentsPaginatedHandler(
    IDepartmentService departmentService,
    IMapper mapper)
    : IQueryHandler<GetDepartmentsPaginatedQuery, ResponseData<GetDepartmentsPaginatedResponse>>
{
    public async Task<Result<ResponseData<GetDepartmentsPaginatedResponse>>> Handle(
        GetDepartmentsPaginatedQuery request,
        CancellationToken cancellationToken)
    {
        var departments = departmentService.GetAllQueryable();

        var resultResponse = await ResponseData<GetDepartmentsPaginatedResponse>.CreateAsync(
            departments,
            request.RequestData,
            mapper,
            cancellationToken);

        return Result.Success(resultResponse);
    }
}
