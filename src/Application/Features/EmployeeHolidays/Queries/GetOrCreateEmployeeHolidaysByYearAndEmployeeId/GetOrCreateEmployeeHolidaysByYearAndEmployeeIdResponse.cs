﻿using AutoMapper;
using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Features.EmployeeHolidays.Queries.GetOrCreateEmployeeHolidaysByYearAndEmployeeId;

public record GetOrCreateEmployeeHolidaysByYearAndEmployeeIdResponse
{
    public Guid Id { get; set; }

    public int Year { get; set; }

    public int TotalDays { get; set; }

    public int ConsumedDays { get; set; }

    public int Available { get; set; }

    public Guid UserId { get; set; }

    public bool Created { get; set; }

    internal class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<EmployeeHoliday, GetOrCreateEmployeeHolidaysByYearAndEmployeeIdResponse>()
                .ForMember(dest => dest.Available, opt => opt.MapFrom(src => src.TotalDays - src.ConsumedDays))
                .ForMember(dest => dest.Created, opt => opt.Ignore());
        }
    }
}
