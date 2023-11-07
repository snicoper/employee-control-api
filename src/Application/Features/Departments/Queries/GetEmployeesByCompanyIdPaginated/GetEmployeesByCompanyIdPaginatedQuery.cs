using EmployeeControl.Application.Common.Models;
using MediatR;

namespace EmployeeControl.Application.Features.Departments.Queries.GetEmployeesByCompanyIdPaginated;

public record GetEmployeesByCompanyIdPaginatedQuery(RequestData RequestData, string CompanyId)
    : IRequest<ResponseData<GetEmployeesByCompanyIdPaginatedResponse>>;
