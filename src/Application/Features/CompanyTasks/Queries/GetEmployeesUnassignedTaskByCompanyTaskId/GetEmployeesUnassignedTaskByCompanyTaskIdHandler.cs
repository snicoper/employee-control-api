using EmployeeControl.Application.Common.Interfaces.Common;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Entities.Identity;
using EmployeeControl.Domain.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl.Application.Features.CompanyTasks.Queries.GetEmployeesUnassignedTaskByCompanyTaskId;

internal class GetEmployeesUnassignedTaskByCompanyTaskIdHandler(
        IApplicationDbContext context,
        IIdentityService identityService,
        ICurrentUserService currentUserService)
    : IRequestHandler<
        GetEmployeesUnassignedTaskByCompanyTaskIdQuery,
        ICollection<GetEmployeesUnassignedTaskByCompanyTaskIdResponse>>
{
    public async Task<ICollection<GetEmployeesUnassignedTaskByCompanyTaskIdResponse>> Handle(
        GetEmployeesUnassignedTaskByCompanyTaskIdQuery request,
        CancellationToken cancellationToken)
    {
        var userCompanyTasks = context
            .UserCompanyTasks
            .Include(uct => uct.User)
            .AsNoTracking()
            .Where(uct => uct.CompanyTaskId != request.Id);

        if (!await identityService.IsInRoleAsync(currentUserService.Id, Roles.Staff))
        {
            userCompanyTasks = userCompanyTasks.Where(uct => uct.CompanyId == currentUserService.CompanyId);
        }

        var resultResponse = userCompanyTasks
            .Where(uct => uct.User != null)
            .Select(uct => new GetEmployeesUnassignedTaskByCompanyTaskIdResponse(
                uct.User!.Id,
                $"{uct.User.FirstName} {uct.User.LastName} <{uct.User.Email}>"))
            .ToList();

        return resultResponse;
    }
}
