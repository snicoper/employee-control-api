using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Features.Departments;
using EmployeeControl.Application.Common.Models;
using MediatR;

namespace EmployeeControl.Application.Features.Departments.Commands.DeactivateDepartment;

internal class DeactivateDepartmentHandler(IDepartmentService departmentService, IEntityValidationService entityValidationService)
    : IRequestHandler<DeactivateDepartmentCommand, Result>
{
    public async Task<Result> Handle(DeactivateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = await departmentService.GetByIdAsync(request.DepartmentId, cancellationToken);
        department.Active = false;

        await entityValidationService.CheckEntityCompanyIsOwnerAsync(department);
        await departmentService.UpdateAsync(department, cancellationToken);

        return Result.Success();
    }
}
