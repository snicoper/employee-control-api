using MediatR;

namespace EmployeeControl.Application.Features.Localization.Queries.CurrentLocale;

public record CurrentLocaleQuery : IRequest<CurrentLocaleDto>;
