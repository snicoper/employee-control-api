using MediatR;

namespace EmployeeControl.Application.Features.TimeControl.Queries.GetTimeControlRangeByEmployeeId;

public record GetTimeControlRangeByEmployeeIdQuery(string EmployeeId, DateTimeOffset From, DateTimeOffset To)
    : IRequest<ICollection<GetTimeControlRangeByEmployeeIdResponse>>;
