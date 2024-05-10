using EmployeeControl.Application.Common.Interfaces.Features.Departments;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;

namespace EmployeeControl.Application.Features.Departments.Commands.DeactivateDepartment;

internal class DeactivateDepartmentHandler(IDepartmentService departmentService)
    : ICommandHandler<DeactivateDepartmentCommand>
{
    public async Task<Result> Handle(DeactivateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = await departmentService.GetByIdAsync(request.DepartmentId, cancellationToken);
        department.Active = false;

        await departmentService.UpdateAsync(department, cancellationToken);

        return Result.Success();
    }
}
