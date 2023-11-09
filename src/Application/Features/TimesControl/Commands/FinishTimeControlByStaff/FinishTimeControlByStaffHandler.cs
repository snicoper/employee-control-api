﻿using EmployeeControl.Application.Common.Interfaces.Features.Identity;
using EmployeeControl.Application.Common.Interfaces.Features.TimesControl;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Enums;
using MediatR;

namespace EmployeeControl.Application.Features.TimesControl.Commands.FinishTimeControlByStaff;

internal class FinishTimeControlByStaffHandler(
    IIdentityService identityService,
    ITimesControlService timesControlService,
    IPermissionsValidationService permissionsValidationService)
    : IRequestHandler<FinishTimeControlByStaffCommand, Result>
{
    public async Task<Result> Handle(FinishTimeControlByStaffCommand request, CancellationToken cancellationToken)
    {
        var timeControl = await timesControlService.GetById(request.TimeControlId, cancellationToken);
        var employee = await identityService.GetByIdAsync(timeControl.UserId);

        await permissionsValidationService.CheckEntityCompanyIsOwnerAsync(timeControl);

        var (result, _) = await timesControlService.FinishAsync(
            employee,
            DeviceType.System,
            ClosedBy.Staff,
            null,
            null,
            cancellationToken);

        return result;
    }
}