using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;

namespace EmployeeControl.Application.Features.TimesControl.Commands.FinishTimeControlByStaff;

[Authorize(Roles = Roles.HumanResources)]
public record FinishTimeControlByStaffCommand(string TimeControlId)
    : ICommand;
