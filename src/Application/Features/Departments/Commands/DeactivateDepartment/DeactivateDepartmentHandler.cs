using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Repositories;

namespace EmployeeControl.Application.Features.Departments.Commands.DeactivateDepartment;

internal class DeactivateDepartmentHandler(IDepartmentRepository departmentRepository)
    : ICommandHandler<DeactivateDepartmentCommand>
{
    public async Task<Result> Handle(DeactivateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = await departmentRepository.GetByIdAsync(request.DepartmentId, cancellationToken);
        department.Active = false;

        await departmentRepository.UpdateAsync(department, cancellationToken);

        return Result.Success();
    }
}
