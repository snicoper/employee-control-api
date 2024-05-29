using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Repositories;

namespace EmployeeControl.Application.Features.Employees.Queries.GetEmployeesPaginated;

internal class GetEmployeesPaginatedHandler(IUserRepository userRepository, IMapper mapper)
    : IQueryHandler<GetEmployeesPaginatedQuery, ResponseData<GetEmployeesPaginatedResponse>>
{
    public async Task<Result<ResponseData<GetEmployeesPaginatedResponse>>> Handle(
        GetEmployeesPaginatedQuery request,
        CancellationToken cancellationToken)
    {
        var users = userRepository.GetAllQueryable();

        var resultResponse = await ResponseData<GetEmployeesPaginatedResponse>.CreateAsync(
            users,
            request.RequestData,
            mapper,
            cancellationToken);

        return Result.Success(resultResponse);
    }
}
