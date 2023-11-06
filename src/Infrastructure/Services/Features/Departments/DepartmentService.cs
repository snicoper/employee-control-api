using EmployeeControl.Application.Common.Interfaces.Common;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Features.Departments;
using EmployeeControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl.Infrastructure.Services.Features.Departments;

public class DepartmentService(
    IApplicationDbContext context,
    IDepartmentValidatorService departmentValidatorService,
    IValidationFailureService validationFailureService)
    : IDepartmentService
{
    public IQueryable<Department> GetAllByCompanyId(string companyId)
    {
        var departments = context
            .Departments
            .AsNoTracking()
            .Where(d => d.CompanyId == companyId);

        return departments;
    }

    public async Task<Department> CreateAsync(Department department, CancellationToken cancellationToken)
    {
        // Validaciones.
        await departmentValidatorService.ValidateNameAsync(department, cancellationToken);
        await departmentValidatorService.ValidateBackgroundAndColorAsync(department, cancellationToken);
        validationFailureService.RaiseExceptionIfExistsErrors();

        // Crear departamento.
        context.Departments.Add(department);
        await context.SaveChangesAsync(cancellationToken);

        return department;
    }
}
