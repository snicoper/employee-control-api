using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Repositories;

namespace EmployeeControl.Application.Features.TimesControl.Queries.GetTimeControlWithEmployeeById;

public class GetTimeControlWithEmployeeByIdHandler(ITimeControlRepository timeControlRepository, IMapper mapper)
    : IQueryHandler<GetTimeControlWithEmployeeByIdQuery, GetTimeControlWithEmployeeByIdResponse>
{
    public async Task<Result<GetTimeControlWithEmployeeByIdResponse>> Handle(
        GetTimeControlWithEmployeeByIdQuery request,
        CancellationToken cancellationToken)
    {
        var timeControl = await timeControlRepository.GetWithEmployeeInfoByIdAsync(request.Id, cancellationToken);
        var resultResponse = mapper.Map<GetTimeControlWithEmployeeByIdResponse>(timeControl);

        return Result.Success(resultResponse);
    }
}
