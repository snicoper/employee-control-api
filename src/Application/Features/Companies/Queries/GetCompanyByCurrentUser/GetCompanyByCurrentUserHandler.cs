using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Repositories;

namespace EmployeeControl.Application.Features.Companies.Queries.GetCompanyByCurrentUser;

internal class GetCompanyByCurrentUserHandler(ICompanyRepository companyRepository)
    : IQueryHandler<GetCompanyByCurrentUserQuery, GetCompanyByCurrentUserResponse>
{
    public async Task<Result<GetCompanyByCurrentUserResponse>> Handle(
        GetCompanyByCurrentUserQuery request,
        CancellationToken cancellationToken)
    {
        var company = await companyRepository.GetCompanyAsync(cancellationToken);
        var result = new GetCompanyByCurrentUserResponse(company.Id, company.Name);

        return Result.Success(result);
    }
}
