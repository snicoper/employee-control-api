using MediatR;

namespace EmployeeControl.Application.Features.Culture.Queries.SupportedCultures;

public record SupportedCulturesQuery : IRequest<SupportedCulturesDto>;
