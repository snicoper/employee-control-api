using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Features.TimesControl;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;

namespace EmployeeControl.Application.Features.TimesControl.Queries.GetTimeControlById;

internal class GetTimeControlByIdHandler(ITimesControlService timesControlService, IMapper mapper)
    : IQueryHandler<GetTimeControlByIdQuery, GetTimeControlByIdResponse>
{
    public async Task<Result<GetTimeControlByIdResponse>> Handle(
        GetTimeControlByIdQuery request,
        CancellationToken cancellationToken)
    {
        var timeControl = await timesControlService.GetByIdAsync(request.Id, cancellationToken);
        var resultResponse = mapper.Map<GetTimeControlByIdResponse>(timeControl);

        return Result.Success(resultResponse);
    }
}
