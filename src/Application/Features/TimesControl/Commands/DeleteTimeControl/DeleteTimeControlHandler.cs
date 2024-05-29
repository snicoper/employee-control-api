using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Repositories;

namespace EmployeeControl.Application.Features.TimesControl.Commands.DeleteTimeControl;

internal class DeleteTimeControlHandler(IApplicationDbContext context, ITimeControlRepository timeControlRepository)
    : ICommandHandler<DeleteTimeControlCommand>
{
    public async Task<Result> Handle(DeleteTimeControlCommand request, CancellationToken cancellationToken)
    {
        var timeControl = await timeControlRepository.GetByIdAsync(request.Id, cancellationToken);
        context.TimeControls.Remove(timeControl);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
