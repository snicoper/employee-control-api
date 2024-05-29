using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Emails;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Entities;
using EmployeeControl.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace EmployeeControl.Application.Features.Departments.Commands.AssignEmployeesToDepartment;

internal class AssignEmployeesToDepartmentHandler(
    IApplicationDbContext context,
    IDepartmentRepository departmentRepository,
    ICompanyRepository companyRepository,
    UserManager<User> userManager,
    IDepartmentEmailsService departmentEmailsService)
    : ICommandHandler<AssignEmployeesToDepartmentCommand>
{
    public async Task<Result> Handle(AssignEmployeesToDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = await departmentRepository.GetByIdAsync(request.Id, cancellationToken);

        var employees = userManager
            .Users
            .Where(au => request.EmployeeIds.Contains(au.Id))
            .ToList();

        var employeesToAdd = employees
            .Select(employee => new EmployeeDepartment { UserId = employee.Id, DepartmentId = department.Id })
            .ToList();

        await context.EmployeeDepartments.AddRangeAsync(employeesToAdd, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        var company = await companyRepository.GetCompanyAsync(cancellationToken);

        // Enviar email a los empleados asignados a la tarea.
        await departmentEmailsService.SendEmployeeAssignDepartmentAsync(department, company, employees);

        return Result.Success();
    }
}
