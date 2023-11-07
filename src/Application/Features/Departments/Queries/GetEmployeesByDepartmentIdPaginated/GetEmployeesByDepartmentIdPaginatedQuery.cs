using EmployeeControl.Application.Common.Models;
using MediatR;

namespace EmployeeControl.Application.Features.Departments.Queries.GetEmployeesByDepartmentIdPaginated;

public record GetEmployeesByDepartmentIdPaginatedQuery(string DepartmentId, RequestData RequestData)
    : IRequest<ResponseData<GetEmployeesByDepartmentIdPaginatedResponse>>;
