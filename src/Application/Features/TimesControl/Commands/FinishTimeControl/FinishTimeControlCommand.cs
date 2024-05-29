using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;
using EmployeeControl.Domain.Enums;

namespace EmployeeControl.Application.Features.TimesControl.Commands.FinishTimeControl;

[Authorize(Roles = Roles.Employee)]
public record FinishTimeControlCommand(Guid EmployeeId, DeviceType DeviceType, double? Latitude, double? Longitude)
    : ICommand;
