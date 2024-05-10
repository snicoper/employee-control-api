using EmployeeControl.Application.Common.Interfaces.Features.Departments;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;

namespace EmployeeControl.Application.Features.Departments.Commands.ActivateDepartment;

internal class ActivateDepartmentHandler(IDepartmentService departmentService)
    : ICommandHandler<ActivateDepartmentCommand>
{
    public async Task<Result> Handle(ActivateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = await departmentService.GetByIdAsync(request.DepartmentId, cancellationToken);
        department.Active = true;

        await departmentService.UpdateAsync(department, cancellationToken);

        return Result.Success();
    }
}
