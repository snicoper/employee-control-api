using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Entities;
using EmployeeControl.Domain.Repositories;

namespace EmployeeControl.Application.Features.Employees.Queries.GetEmployeeById;

public class GetEmployeeByIdHandler(IUserRepository userRepository, IMapper mapper)
    : IQueryHandler<GetEmployeeByIdQuery, GetEmployeeByIdResponse>
{
    public async Task<Result<GetEmployeeByIdResponse>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        var employee = await userRepository.GetByIdWithCompanyCalendarAsync(request.Id);
        var result = mapper.Map<User, GetEmployeeByIdResponse>(employee);

        return Result.Success(result);
    }
}
