using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;
using EmployeeControl.Domain.Enums;

namespace EmployeeControl.Application.Features.TimesControl.Commands.StartTimeControl;

[Authorize(Roles = Roles.Employee)]
public record StartTimeControlCommand(string EmployeeId, DeviceType DeviceType, double? Latitude, double? Longitude)
    : ICommand<string>;
