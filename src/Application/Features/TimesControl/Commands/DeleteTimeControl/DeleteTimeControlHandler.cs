using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Features.TimesControl;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;

namespace EmployeeControl.Application.Features.TimesControl.Commands.DeleteTimeControl;

internal class DeleteTimeControlHandler(IApplicationDbContext context, ITimesControlService timesControlService)
    : ICommandHandler<DeleteTimeControlCommand>
{
    public async Task<Result> Handle(DeleteTimeControlCommand request, CancellationToken cancellationToken)
    {
        var timeControl = await timesControlService.GetByIdAsync(request.Id, cancellationToken);
        context.TimeControls.Remove(timeControl);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
