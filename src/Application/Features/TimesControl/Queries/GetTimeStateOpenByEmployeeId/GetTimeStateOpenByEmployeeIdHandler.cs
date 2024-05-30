using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Entities;
using EmployeeControl.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl.Application.Features.TimesControl.Queries.GetTimeStateOpenByEmployeeId;

internal class GetTimeStateOpenByEmployeeIdHandler(IApplicationDbContext context)
    : IQueryHandler<GetTimeStateOpenByEmployeeIdQuery, GetTimeStateOpenByEmployeeIdResponse>
{
    public async Task<Result<GetTimeStateOpenByEmployeeIdResponse>> Handle(
        GetTimeStateOpenByEmployeeIdQuery request,
        CancellationToken cancellationToken)
    {
        var timeControl = await context
                .TimeControls
                .SingleOrDefaultAsync(
                    ct => ct.UserId == request.EmployeeId && ct.TimeState == TimeState.Open,
                    cancellationToken)
            ?? new TimeControl { TimeState = TimeState.Close };

        var resultResponse = new GetTimeStateOpenByEmployeeIdResponse(timeControl.Start, timeControl.TimeState);

        return Result.Success(resultResponse);
    }
}
