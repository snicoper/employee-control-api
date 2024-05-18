using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Repositories;

namespace EmployeeControl.Application.Features.TimesControl.Queries.GetTimeControlById;

internal class GetTimeControlByIdHandler(ITimeControlRepository timeControlRepository, IMapper mapper)
    : IQueryHandler<GetTimeControlByIdQuery, GetTimeControlByIdResponse>
{
    public async Task<Result<GetTimeControlByIdResponse>> Handle(
        GetTimeControlByIdQuery request,
        CancellationToken cancellationToken)
    {
        var timeControl = await timeControlRepository.GetByIdAsync(request.Id, cancellationToken);
        var resultResponse = mapper.Map<GetTimeControlByIdResponse>(timeControl);

        return Result.Success(resultResponse);
    }
}
