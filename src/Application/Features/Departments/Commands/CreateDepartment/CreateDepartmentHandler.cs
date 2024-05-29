using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Entities;
using EmployeeControl.Domain.Repositories;

namespace EmployeeControl.Application.Features.Departments.Commands.CreateDepartment;

internal class CreateDepartmentHandler(IDepartmentRepository departmentRepository, IMapper mapper)
    : ICommandHandler<CreateDepartmentCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = mapper.Map<Department>(request);
        department.Active = true;

        department = await departmentRepository.CreateAsync(department, cancellationToken);

        return Result.Success(department.Id);
    }
}
