using AutoMapper;

namespace EmployeeControl.Application.Features.TimeControl.Queries.GetTimeControlRangeByEmployeeId;

public record GetTimeControlRangeByEmployeeIdResponse(string Id, DateTimeOffset Start, DateTimeOffset Finish)
{
    internal class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Domain.Entities.TimeControl, GetTimeControlRangeByEmployeeIdResponse>();
        }
    }
}
