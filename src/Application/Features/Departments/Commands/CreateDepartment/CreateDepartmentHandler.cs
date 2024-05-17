﻿using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Entities;
using EmployeeControl.Domain.Repositories;

namespace EmployeeControl.Application.Features.Departments.Commands.CreateDepartment;

internal class CreateDepartmentHandler(IDepartmentRepository departmentRepository, IMapper mapper)
    : ICommandHandler<CreateDepartmentCommand, string>
{
    public async Task<Result<string>> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = mapper.Map<Department>(request);
        department.Active = true;

        department = await departmentRepository.CreateAsync(department, cancellationToken);

        return Result.Success(department.Id);
    }
}
