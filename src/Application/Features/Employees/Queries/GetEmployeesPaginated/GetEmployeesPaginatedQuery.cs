using EmployeeControl.Application.Common.Models;
using MediatR;

namespace EmployeeControl.Application.Features.Employees.Queries.GetEmployeesPaginated;

public record GetEmployeesPaginatedQuery(RequestData RequestData) : IRequest<ResponseData<GetEmployeesPaginatedResponse>>;
