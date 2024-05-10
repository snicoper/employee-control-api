using AutoMapper;
using EmployeeControl.Application.Common.Exceptions;
using EmployeeControl.Application.Common.Interfaces.Common;
using EmployeeControl.Application.Common.Interfaces.Features.Identity;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Features.Employees.Commands.UpdateEmployeeSettings;

internal class UpdateEmployeeSettingsHandler(
    IEmployeeSettingsService employeeSettingsService,
    ICurrentUserService currentUserService,
    IMapper mapper)
    : ICommandHandler<UpdateEmployeeSettingsCommand, EmployeeSettings>
{
    public async Task<Result<EmployeeSettings>> Handle(UpdateEmployeeSettingsCommand request, CancellationToken cancellationToken)
    {
        // Comprobar que el empleado actualiza su propia configuración.
        if (currentUserService.Id != request.UserId)
        {
            throw new NotFoundException(nameof(EmployeeSettings), nameof(EmployeeSettings.UserId));
        }

        var employeeSettings = mapper.Map<EmployeeSettings>(request);
        await employeeSettingsService.GetByEmployeeIdAsync(request.UserId, cancellationToken);
        var result = await employeeSettingsService.UpdateAsync(employeeSettings, cancellationToken);

        return Result.Success(result);
    }
}
