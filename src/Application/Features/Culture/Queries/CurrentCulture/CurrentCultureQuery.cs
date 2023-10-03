using MediatR;

namespace EmployeeControl.Application.Features.Culture.Queries.CurrentCulture;

public record CurrentCultureQuery : IRequest<CurrentCultureDto>;
