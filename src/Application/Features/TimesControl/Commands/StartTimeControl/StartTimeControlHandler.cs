using EmployeeControl.Application.Common.Interfaces.Features.TimesControl;
using EmployeeControl.Application.Common.Models;
using MediatR;

namespace EmployeeControl.Application.Features.TimesControl.Commands.StartTimeControl;

internal class StartTimeControlHandler(ITimesControlService timesControlService)
    : IRequestHandler<StartTimeControlCommand, Result>
{
    public async Task<Result> Handle(StartTimeControlCommand request, CancellationToken cancellationToken)
    {
        var (result, _) = await timesControlService.StartAsync(request.EmployeeId, cancellationToken);

        return result;
    }
}
