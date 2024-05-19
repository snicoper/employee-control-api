using AutoMapper;
using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Features.EmployeeHolidays.Queries.GetEmployeeHolidaysByYearPaginated;

public record GetEmployeeHolidaysByYearPaginatedResponse
{
    public string Id { get; set; } = default!;

    public int Year { get; set; }

    public int TotalDays { get; set; }

    public int ConsumedDays { get; set; }

    public int Remaining { get; set; }

    public string UserId { get; set; } = default!;

    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    public string Email { get; set; } = default!;

    internal class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<User, GetEmployeeHolidaysByYearPaginatedResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.EmployeeHolidays.FirstOrDefault()!.Id))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.EmployeeHolidays.FirstOrDefault()!.Year))
                .ForMember(dest => dest.TotalDays, opt => opt.MapFrom(src => src.EmployeeHolidays.FirstOrDefault()!.TotalDays))
                .ForMember(dest => dest.ConsumedDays, opt => opt.MapFrom(src => src.EmployeeHolidays.FirstOrDefault()!.ConsumedDays))
                .ForMember(dest => dest.Remaining, opt => opt.MapFrom(src => Remaining(src)))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));
        }

        private static int Remaining(User user)
        {
            var remaining = user.EmployeeHolidays.FirstOrDefault()!.TotalDays -
                            user.EmployeeHolidays.FirstOrDefault()!.ConsumedDays;

            return remaining;
        }
    }
}
