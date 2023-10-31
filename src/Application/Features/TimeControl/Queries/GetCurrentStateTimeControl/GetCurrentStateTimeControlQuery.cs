using MediatR;

namespace EmployeeControl.Application.Features.TimeControl.Queries.GetCurrentStateTimeControl;

public record GetCurrentStateTimeControlQuery(string EmployeeId) : IRequest<GetCurrentStateTimeControlResponse>;
