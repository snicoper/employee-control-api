using EmployeeControl.Application.Common.Extensions;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Entities;
using EmployeeControl.Domain.Exceptions;
using EmployeeControl.Domain.Repositories;
using EmployeeControl.Domain.Validators;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl.Infrastructure.Repositories;

public class DepartmentRepository(IApplicationDbContext context, IDepartmentValidator departmentValidator)
    : IDepartmentRepository
{
    public IQueryable<Department> GetAllQueryable()
    {
        var departments = context.Departments;

        return departments;
    }

    public IQueryable<Department> GetAllByEmployeeIdQueryable(Guid employeeId)
    {
        var department = context
            .EmployeeDepartments
            .Include(ud => ud.Department)
            .Where(ud => ud.UserId == employeeId)
            .Select(ud => ud.Department);

        return department;
    }

    public async Task<Department> GetByIdAsync(Guid departmentId, CancellationToken cancellationToken)
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
        result = await departmentValidator.ValidateNameAsync(department, result, cancellationToken);
        result = await departmentValidator.ValidateBackgroundAndColorAsync(department, result, cancellationToken);
        result.RaiseBadRequestIfErrorsExist();

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
