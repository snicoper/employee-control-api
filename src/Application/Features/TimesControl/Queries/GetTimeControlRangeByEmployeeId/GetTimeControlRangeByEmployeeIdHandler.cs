using EmployeeControl.Application.Common.Interfaces.Features.TimesControl;
using EmployeeControl.Domain.Entities;
using EmployeeControl.Domain.Enums;
using MediatR;

namespace EmployeeControl.Application.Features.TimesControl.Queries.GetTimeControlRangeByEmployeeId;

internal class GetTimeControlRangeByEmployeeIdHandler(TimeProvider timeProvider, ITimesControlService timesControlService)
    : IRequestHandler<GetTimeControlRangeByEmployeeIdQuery, ICollection<GetTimeControlRangeByEmployeeIdResponse>>
{
    public async Task<ICollection<GetTimeControlRangeByEmployeeIdResponse>> Handle(
        GetTimeControlRangeByEmployeeIdQuery request,
        CancellationToken cancellationToken)
    {
        // Si algún tiempo Finish no esta cerrado, pone el tiempo actual y lo suma al total.
        var timesControlGroup = await timesControlService.GetRangeByEmployeeIdAsync(
            request.EmployeeId,
            request.From,
            request.To,
            cancellationToken);

        var resultResponse = timesControlGroup
            .Select(TimesControlMap)
            .ToList();

        return resultResponse;
    }

    private GetTimeControlRangeByEmployeeIdResponse TimesControlMap(IGrouping<int, TimeControl> timesControlGroup)
    {
        const int minutesInDay = 60 * 24;
        var timeControlResponse = new GetTimeControlRangeByEmployeeIdResponse();
        var totalMinutes = TimeSpan.Zero;

        foreach (var control in timesControlGroup)
        {
            var unclosed = control.ClosedBy == ClosedBy.Unclosed;
            var timeFinish = unclosed ? timeProvider.GetUtcNow() : control.Finish;
            var diff = timeFinish - control.Start;
            var percent = (int)Math.Floor(diff.TotalMinutes / minutesInDay * 100);
            var minutes = (int)Math.Floor(diff.TotalMinutes);

            timeControlResponse.Times.Add(new GetTimeControlRangeByEmployeeIdResponse.TimeControlResponse(
                control.Id,
                control.Start,
                control.Finish,
                unclosed,
                minutes,
                percent));

            totalMinutes += diff;
        }

        timeControlResponse.TotalMinutes = (int)Math.Floor(totalMinutes.TotalMinutes);
        timeControlResponse.Day = timesControlGroup.Key;

        return timeControlResponse;
    }
}
