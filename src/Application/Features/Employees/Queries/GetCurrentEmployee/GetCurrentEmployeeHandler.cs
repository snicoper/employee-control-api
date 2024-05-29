using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Repositories;

namespace EmployeeControl.Application.Features.Employees.Queries.GetCurrentEmployee;

internal class GetCurrentEmployeeHandler(IUserRepository userRepository, IMapper mapper)
    : IQueryHandler<GetCurrentEmployeeQuery, GetCurrentEmployeeResponse>
{
    public async Task<Result<GetCurrentEmployeeResponse>> Handle(
        GetCurrentEmployeeQuery request,
        CancellationToken cancellationToken)
    {
        var user = await userRepository.GetCurrentAsync();
        var resultResponse = mapper.Map<GetCurrentEmployeeResponse>(user);

        return Result.Success(resultResponse);
    }
}
