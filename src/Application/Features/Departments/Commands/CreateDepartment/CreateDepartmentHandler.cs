using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Features.Departments;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Features.Departments.Commands.CreateDepartment;

internal class CreateDepartmentHandler(IDepartmentService departmentService, IMapper mapper)
    : ICommandHandler<CreateDepartmentCommand, string>
{
    public async Task<Result<string>> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = mapper.Map<Department>(request);
        department.Active = true;

        department = await departmentService.CreateAsync(department, cancellationToken);

        return Result.Success(department.Id);
    }
}
