using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Emails;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Entities;
using EmployeeControl.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace EmployeeControl.Application.Features.CompanyTasks.Commands.AssignEmployeesToTask;

internal class AssignEmployeesToTaskHandler(
    IApplicationDbContext context,
    ICompanyTaskRepository companyTaskRepository,
    ICompanyRepository companyRepository,
    UserManager<User> userManager,
    ICompanyTaskEmailsService companyTaskEmailsService)
    : ICommandHandler<AssignEmployeesToTaskCommand>
{
    public async Task<Result> Handle(AssignEmployeesToTaskCommand request, CancellationToken cancellationToken)
    {
        var companyTask = await companyTaskRepository.GetByIdAsync(request.Id, cancellationToken);

        var employees = userManager
            .Users
            .Where(au => request.EmployeeIds.Contains(au.Id))
            .ToList();

        var userCompanyTasks = employees
            .Select(employee => new EmployeeCompanyTask { UserId = employee.Id, CompanyTaskId = companyTask.Id })
            .ToList();

        await context.EmployeeCompanyTasks.AddRangeAsync(userCompanyTasks, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        var company = await companyRepository.GetCompanyAsync(cancellationToken);

        // Enviar email a los empleados asignados a la tarea.
        await companyTaskEmailsService.SendEmployeeAssignTaskAsync(companyTask, company, employees);

        return Result.Success();
    }
}
