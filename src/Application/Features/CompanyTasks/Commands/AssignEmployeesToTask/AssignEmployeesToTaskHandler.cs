using EmployeeControl.Application.Common.Exceptions;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Features.CompanyTask;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl.Application.Features.CompanyTasks.Commands.AssignEmployeesToTask;

internal class AssignEmployeesToTaskHandler(
    IApplicationDbContext context,
    UserManager<ApplicationUser> userManager,
    IEntityValidationService entityValidationService,
    ICompanyTaskEmailsService companyTaskEmailsService)
    : IRequestHandler<AssignEmployeesToTaskCommand, Result>
{
    public async Task<Result> Handle(AssignEmployeesToTaskCommand request, CancellationToken cancellationToken)
    {
        var companyTask = await context
            .CompanyTasks
            .AsNoTracking()
            .Include(ct => ct.Company)
            .SingleOrDefaultAsync(ct => ct.Id == request.Id, cancellationToken);

        if (companyTask?.Company is null)
        {
            throw new NotFoundException(nameof(CompanyTask), nameof(CompanyTask.Id));
        }

        await entityValidationService.CheckEntityCompanyIsOwnerAsync(companyTask);

        var employees = userManager
            .Users
            .AsNoTracking()
            .Where(au => request.EmployeeIds.Contains(au.Id) && au.CompanyId == companyTask.CompanyId)
            .ToList();

        var userCompanyTasks = new List<UserCompanyTask>();

        foreach (var employee in employees)
        {
            userCompanyTasks.Add(new UserCompanyTask
            {
                CompanyId = companyTask.CompanyId, UserId = employee.Id, CompanyTaskId = companyTask.Id
            });
        }

        await context.UserCompanyTasks.AddRangeAsync(userCompanyTasks, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        // Enviar email a los empleados asignados a la tarea.
        await companyTaskEmailsService.SendEmployeeAssignTaskAsync(companyTask, companyTask.Company, employees);

        return Result.Success();
    }
}
