using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Repositories;

namespace EmployeeControl.Application.Features.Departments.Queries.GetDepartmentById;

internal class GetDepartmentByIdHandler(
    IDepartmentRepository departmentRepository,
    IMapper mapper)
    : IQueryHandler<GetDepartmentByIdQuery, GetDepartmentByIdResponse>
{
    public async Task<Result<GetDepartmentByIdResponse>> Handle(
        GetDepartmentByIdQuery request,
        CancellationToken cancellationToken)
    {
        var department = await departmentRepository.GetByIdAsync(request.DepartmentId, cancellationToken);
        var resultResponse = mapper.Map<GetDepartmentByIdResponse>(department);

        return Result.Success(resultResponse);
    }
}
