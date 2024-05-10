using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Features.TimesControl;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;

namespace EmployeeControl.Application.Features.TimesControl.Queries.GetTimeControlWithEmployeeById;

public class GetTimeControlWithEmployeeByIdHandler(ITimesControlService timesControlService, IMapper mapper)
    : IQueryHandler<GetTimeControlWithEmployeeByIdQuery, GetTimeControlWithEmployeeByIdResponse>
{
    public async Task<Result<GetTimeControlWithEmployeeByIdResponse>> Handle(
        GetTimeControlWithEmployeeByIdQuery request,
        CancellationToken cancellationToken)
    {
        var timeControl = await timesControlService.GetWithEmployeeInfoByIdAsync(request.Id, cancellationToken);
        var resultResponse = mapper.Map<GetTimeControlWithEmployeeByIdResponse>(timeControl);

        return Result.Success(resultResponse);
    }
}
