using EmployeeControl.Application.Common.Interfaces.Features.TimesControl;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;

namespace EmployeeControl.Application.Features.TimesControl.Commands.CloseIncidence;

internal class CloseIncidenceHandler(ITimesControlService timesControlService)
    : ICommandHandler<CloseIncidenceCommand>
{
    public async Task<Result> Handle(CloseIncidenceCommand request, CancellationToken cancellationToken)
    {
        await timesControlService.CloseIncidenceByTimeControlIdAsync(request.Id, cancellationToken);

        return Result.Success();
    }
}
