using MediatR;

namespace EmployeeControl.Application.Features.CompanyTasks.Queries.GetCompanyTasksByIdAndCompanyId;

public record GetCompanyTasksByIdAndCompanyIdQuery(int Id, int CompanyId) : IRequest<GetCompanyTasksByIdAndCompanyIdResponse>;
