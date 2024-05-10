using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Features.Departments;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;

namespace EmployeeControl.Application.Features.Departments.Commands.UpdateDepartment;

internal class UpdateDepartmentHandler(
    IDepartmentService departmentService,
    IDepartmentValidatorService departmentValidatorService,
    IMapper mapper)
    : ICommandHandler<UpdateDepartmentCommand>
{
    public async Task<Result> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = await departmentService.GetByIdAsync(request.Id, cancellationToken);

        var departmentUpdate = mapper.Map(request, department);

        var result = Result.Create();
        await departmentValidatorService.ValidateNameAsync(department, result, cancellationToken);
        await departmentValidatorService.ValidateBackgroundAndColorAsync(department, result, cancellationToken);
        result.RaiseBadRequestIfResultFailure();

        await departmentService.UpdateAsync(departmentUpdate, cancellationToken);

        return result;
    }
}
