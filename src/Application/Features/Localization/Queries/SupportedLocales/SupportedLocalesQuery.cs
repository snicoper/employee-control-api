using MediatR;

namespace EmployeeControl.Application.Features.Localization.Queries.SupportedLocales;

public record SupportedLocalesQuery : IRequest<SupportedLocalesResponse>;
