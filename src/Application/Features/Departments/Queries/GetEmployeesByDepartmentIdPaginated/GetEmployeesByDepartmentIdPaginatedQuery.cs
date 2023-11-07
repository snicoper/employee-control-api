using EmployeeControl.Application.Common.Models;
using MediatR;

namespace EmployeeControl.Application.Features.Departments.Queries.GetEmployeesByDepartmentIdPaginated;

public record GetEmployeesByDepartmentIdPaginatedQuery(RequestData RequestData, string DepartmentId)
    : IRequest<ResponseData<GetEmployeesByDepartmentIdPaginatedResponse>>;
