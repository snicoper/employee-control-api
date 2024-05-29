using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Repositories;

namespace EmployeeControl.Application.Features.WorkingDaysWeek.Commands.UpdateWorkingDaysWeek;

internal class UpdateWorkingDaysWeekHandler(IWorkingDaysWeekRepository workingDaysWeekRepository, IMapper mapper)
    : ICommandHandler<UpdateWorkingDaysWeekCommand>
{
    public async Task<Result> Handle(UpdateWorkingDaysWeekCommand request, CancellationToken cancellationToken)
    {
        var workingDaysWeek = await workingDaysWeekRepository.GetWorkingDaysWeekAsync(cancellationToken);
        workingDaysWeek = mapper.Map(request, workingDaysWeek);

        await workingDaysWeekRepository.UpdateAsync(workingDaysWeek, cancellationToken);

        return Result.Success();
    }
}
