using AutoMapper;
using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Features.TimesControl.Queries.GetTimeControlRangeByEmployeeId;

public record GetTimeControlRangeByEmployeeIdResponse
{
    public int Day { get; set; }

    public ICollection<TimeControlResponse> Times { get; set; } = new List<TimeControlResponse>();

    public record TimeControlResponse(string Id, DateTimeOffset Start, DateTimeOffset? Finish)
    {
        internal class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<TimeControl, TimeControlResponse>()
                    .ForAllMembers(opts =>
                    {
                        opts.Condition((_, _, srcMember) => srcMember != null);
                    });
            }
        }
    }
}
