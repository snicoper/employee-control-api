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
        var timesControlGroup = await timesControlService.GetTimeControlRangeByEmployeeIdAsync(
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
        const int dayMinutes = 60 * 24;
        var timeControlResponse = new GetTimeControlRangeByEmployeeIdResponse();

        // TODO: Falta comprobar si el tiempo ha superado las 00:00:00, el sistema debería cerrarlo.
        // Establecer la hora actual, si no tiene un tiempo cerrado y sumar el grupo.
        var totalMinutes = TimeSpan.Zero;

        foreach (var control in timesControlGroup)
        {
            var unclosed = control.ClosedBy == ClosedBy.Unclosed;
            var timeFinish = unclosed ? timeProvider.GetUtcNow() : control.Finish;
            var diff = timeFinish - control.Start;
            var percent = (int)Math.Floor(diff.TotalMinutes / dayMinutes * 100);

            totalMinutes += diff;

            timeControlResponse.Times.Add(new GetTimeControlRangeByEmployeeIdResponse.TimeControlResponse(
                control.Id,
                control.Start,
                control.Finish,
                unclosed,
                (int)Math.Floor(diff.TotalMinutes),
                percent));
        }

        timeControlResponse.TotalMinutes = (int)Math.Floor(totalMinutes.TotalMinutes);
        timeControlResponse.Day = timesControlGroup.Key;

        return timeControlResponse;
    }
}
