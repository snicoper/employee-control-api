using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Features.Departments;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Repositories;

namespace EmployeeControl.Application.Features.Departments.Commands.UpdateDepartment;

internal class UpdateDepartmentHandler(
    IDepartmentRepository departmentRepository,
    IDepartmentValidatorService departmentValidatorService,
    IMapper mapper)
    : ICommandHandler<UpdateDepartmentCommand>
{
    public async Task<Result> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = await departmentRepository.GetByIdAsync(request.Id, cancellationToken);

        var departmentUpdate = mapper.Map(request, department);

        var result = Result.Create();
        await departmentValidatorService.ValidateNameAsync(department, result, cancellationToken);
        await departmentValidatorService.ValidateBackgroundAndColorAsync(department, result, cancellationToken);
        result.RaiseBadRequestIfResultFailure();

        await departmentRepository.UpdateAsync(departmentUpdate, cancellationToken);

        return result;
    }
}
