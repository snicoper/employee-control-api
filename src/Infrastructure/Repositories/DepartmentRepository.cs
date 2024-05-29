using EmployeeControl.Application.Common.Extensions;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Features.Departments;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Entities;
using EmployeeControl.Domain.Exceptions;
using EmployeeControl.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl.Infrastructure.Repositories;

public class DepartmentRepository(IApplicationDbContext context, IDepartmentValidatorService departmentValidatorService)
    : IDepartmentRepository
{
    public IQueryable<Department> GetAllQueryable()
    {
        var departments = context.Departments;

        return departments;
    }

    public IQueryable<Department> GetAllByEmployeeIdQueryable(string employeeId)
    {
        var department = context
            .EmployeeDepartments
            .Include(ud => ud.Department)
            .Where(ud => ud.UserId == employeeId)
            .Select(ud => ud.Department);

        return department;
    }

    public async Task<Department> GetByIdAsync(string departmentId, CancellationToken cancellationToken)
    {
        var department = await context
                             .Departments
                             .SingleOrDefaultAsync(d => d.Id == departmentId, cancellationToken)
                         ?? throw new NotFoundException(nameof(Department), nameof(Department.Id));

        return department;
    }

    public async Task<Department> CreateAsync(Department department, CancellationToken cancellationToken)
    {
        // Validaciones.
        var result = Result.Create();
        result = await departmentValidatorService.ValidateNameAsync(department, result, cancellationToken);
        result = await departmentValidatorService.ValidateBackgroundAndColorAsync(department, result, cancellationToken);
        result.RaiseBadRequest();

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
