using EmployeeControl.Application.Common.Exceptions;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Features.Departments;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl.Infrastructure.Services.Features.Departments;

public class DepartmentService(IApplicationDbContext context, IDepartmentValidatorService departmentValidatorService)
    : IDepartmentService
{
    public IQueryable<Department> GetAllQueryable()
    {
        var departments = context.Departments;

        return departments;
    }

    public IQueryable<Department> GetAllByEmployeeIdQueryable(string employeeId)
    {
        var departments = context
            .EmployeeDepartments
            .Include(ud => ud.Department)
            .Where(ud => ud.UserId == employeeId)
            .Select(ud => ud.Department);

        return departments;
    }

    public async Task<Department> GetByIdAsync(string departmentId, CancellationToken cancellationToken)
    {
        var department = await context
                             .Departments
                             .SingleOrDefaultAsync(d => d.Id == departmentId, cancellationToken) ??
                         throw new NotFoundException(nameof(Department), nameof(Department.Id));

        return department;
    }

    public async Task<Department> CreateAsync(Department department, CancellationToken cancellationToken)
    {
        // Validaciones.
        var result = Result.Create();
        await departmentValidatorService.ValidateNameAsync(department, result, cancellationToken);
        await departmentValidatorService.ValidateBackgroundAndColorAsync(department, result, cancellationToken);
        result.RaiseBadRequestIfResultFailure();

        // Crear departamento.
        context.Departments.Add(department);
        await context.SaveChangesAsync(cancellationToken);

        return department;
    }

    public async Task<Department> UpdateAsync(Department department, CancellationToken cancellationToken)
    {
        context.Departments.Update(department);
        await context.SaveChangesAsync(cancellationToken);

        return department;
    }
}
