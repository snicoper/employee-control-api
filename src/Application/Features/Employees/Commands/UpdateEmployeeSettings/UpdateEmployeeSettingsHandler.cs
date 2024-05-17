using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Interfaces.Users;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Entities;
using EmployeeControl.Domain.Exceptions;
using EmployeeControl.Domain.Repositories;

namespace EmployeeControl.Application.Features.Employees.Commands.UpdateEmployeeSettings;

internal class UpdateEmployeeSettingsHandler(
    IEmployeeSettingsRepository employeeSettingsRepository,
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
        await employeeSettingsRepository.GetByEmployeeIdAsync(request.UserId, cancellationToken);
        var result = await employeeSettingsRepository.UpdateAsync(employeeSettings, cancellationToken);

        return Result.Success(result);
    }
}
