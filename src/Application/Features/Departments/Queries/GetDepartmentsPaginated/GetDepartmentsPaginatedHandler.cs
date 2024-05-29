using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Repositories;

namespace EmployeeControl.Application.Features.Departments.Queries.GetDepartmentsPaginated;

internal class GetDepartmentsPaginatedHandler(
    IDepartmentRepository departmentRepository,
    IMapper mapper)
    : IQueryHandler<GetDepartmentsPaginatedQuery, ResponseData<GetDepartmentsPaginatedResponse>>
{
    public async Task<Result<ResponseData<GetDepartmentsPaginatedResponse>>> Handle(
        GetDepartmentsPaginatedQuery request,
        CancellationToken cancellationToken)
    {
        var departments = departmentRepository.GetAllQueryable();

        var resultResponse = await ResponseData<GetDepartmentsPaginatedResponse>.CreateAsync(
            departments,
            request.RequestData,
            mapper,
            cancellationToken);

        return Result.Success(resultResponse);
    }
}
