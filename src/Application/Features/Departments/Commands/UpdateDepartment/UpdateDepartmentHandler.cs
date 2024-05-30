using AutoMapper;
using EmployeeControl.Application.Common.Extensions;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Repositories;
using EmployeeControl.Domain.Validators;

namespace EmployeeControl.Application.Features.Departments.Commands.UpdateDepartment;

internal class UpdateDepartmentHandler(
    IDepartmentRepository departmentRepository,
    IDepartmentValidator departmentValidator,
    IMapper mapper)
    : ICommandHandler<UpdateDepartmentCommand>
{
    public async Task<Result> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = await departmentRepository.GetByIdAsync(request.Id, cancellationToken);

        var departmentUpdate = mapper.Map(request, department);

        var result = Result.Create();
        await departmentValidator.ValidateNameAsync(department, result, cancellationToken);
        await departmentValidator.ValidateBackgroundAndColorAsync(department, result, cancellationToken);
        result.RaiseBadRequestIfErrorsExist();

        await departmentRepository.UpdateAsync(departmentUpdate, cancellationToken);

        return result;
    }
}
