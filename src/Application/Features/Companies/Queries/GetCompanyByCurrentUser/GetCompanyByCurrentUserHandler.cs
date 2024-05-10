using EmployeeControl.Application.Common.Interfaces.Features.Companies;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;

namespace EmployeeControl.Application.Features.Companies.Queries.GetCompanyByCurrentUser;

internal class GetCompanyByCurrentUserHandler(ICompanyService companyService)
    : IQueryHandler<GetCompanyByCurrentUserQuery, GetCompanyByCurrentUserResponse>
{
    public async Task<Result<GetCompanyByCurrentUserResponse>> Handle(
        GetCompanyByCurrentUserQuery request,
        CancellationToken cancellationToken)
    {
        var company = await companyService.GetCompanyAsync(cancellationToken);
        var result = new GetCompanyByCurrentUserResponse(company.Id, company.Name);

        return Result.Success(result);
    }
}
