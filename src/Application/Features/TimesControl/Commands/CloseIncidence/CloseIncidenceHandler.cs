using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Repositories;

namespace EmployeeControl.Application.Features.TimesControl.Commands.CloseIncidence;

internal class CloseIncidenceHandler(ITimeControlRepository timeControlRepository)
    : ICommandHandler<CloseIncidenceCommand>
{
    public async Task<Result> Handle(CloseIncidenceCommand request, CancellationToken cancellationToken)
    {
        await timeControlRepository.CloseIncidenceByTimeControlIdAsync(request.Id, cancellationToken);

        return Result.Success();
    }
}
