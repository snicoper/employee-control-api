using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Repositories;

namespace EmployeeControl.Application.Features.Departments.Commands.ActivateDepartment;

internal class ActivateDepartmentHandler(IDepartmentRepository departmentRepository) : ICommandHandler<ActivateDepartmentCommand>
{
    public async Task<Result> Handle(ActivateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = await departmentRepository.GetByIdAsync(request.DepartmentId, cancellationToken);
        department.Active = true;

        await departmentRepository.UpdateAsync(department, cancellationToken);

        return Result.Success();
    }
}
