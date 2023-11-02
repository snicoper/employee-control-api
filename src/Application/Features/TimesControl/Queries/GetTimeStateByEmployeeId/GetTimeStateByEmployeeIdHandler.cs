using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Features.TimesControl;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl.Application.Features.TimesControl.Queries.GetTimeStateByEmployeeId;

internal class GetTimeStateByEmployeeIdHandler(ITimesControlService timesControlService, IApplicationDbContext context)
    : IRequestHandler<GetTimeStateByEmployeeIdQuery, GetTimeStateByEmployeeIdResponse>
{
    public async Task<GetTimeStateByEmployeeIdResponse> Handle(
        GetTimeStateByEmployeeIdQuery request,
        CancellationToken cancellationToken)
    {
        var prueba = context
            .CompanySettings
            .Include(c => c.Company)
            .ToList();

        var timeState = await timesControlService.GetTimeStateByEmployeeIAsync(request.EmployeeId, cancellationToken);
        var resultResponse = new GetTimeStateByEmployeeIdResponse(timeState);

        return resultResponse;
    }
}
