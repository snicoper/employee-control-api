using EmployeeControl.Application.Common.Exceptions;
using EmployeeControl.Application.Common.Interfaces.Features.TimesControl;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Entities;
using EmployeeControl.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EmployeeControl.Application.Features.TimesControl.Commands.FinishTimeControl;

internal class FinishTimeControlHandler(UserManager<ApplicationUser> userManager, ITimesControlService timesControlService)
    : IRequestHandler<FinishTimeControlCommand, Result>
{
    public async Task<Result> Handle(FinishTimeControlCommand request, CancellationToken cancellationToken)
    {
        _ = await userManager.FindByIdAsync(request.EmployeeId) ??
            throw new NotFoundException(nameof(ApplicationUser), nameof(ApplicationUser.Id));

        var (result, _) = await timesControlService
            .FinishAsync(request.EmployeeId, request.DeviceType, ClosedBy.Employee, cancellationToken);

        return result;
    }
}
