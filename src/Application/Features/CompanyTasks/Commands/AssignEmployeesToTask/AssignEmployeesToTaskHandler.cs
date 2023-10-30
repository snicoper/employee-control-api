using EmployeeControl.Application.Common.Exceptions;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Entities;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl.Application.Features.CompanyTasks.Commands.AssignEmployeesToTask;

internal class AssignEmployeesToTaskHandler(
        IApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        IEntityValidationService entityValidationService)
    : IRequestHandler<AssignEmployeesToTaskCommand, Result>
{
    public async Task<Result> Handle(AssignEmployeesToTaskCommand request, CancellationToken cancellationToken)
    {
        var companyTask = await context
                              .CompanyTasks
                              .AsNoTracking()
                              .FirstOrDefaultAsync(ct => ct.Id == request.Id, cancellationToken) ??
                          throw new NotFoundException(nameof(CompanyTask), nameof(CompanyTask.Id));

        await entityValidationService.CheckEntityCompanyIsOwner(companyTask);

        var employees = userManager
            .Users
            .AsNoTracking()
            .Where(au => request.EmployeeIds.Contains(au.Id) && au.CompanyId == companyTask.CompanyId);

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

        return Result.Success();
    }
}
