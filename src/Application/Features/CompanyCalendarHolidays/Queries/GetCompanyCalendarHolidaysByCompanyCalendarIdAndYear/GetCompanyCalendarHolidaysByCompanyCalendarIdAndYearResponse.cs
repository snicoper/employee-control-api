﻿using AutoMapper;
using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Features.CompanyCalendarHolidays.Queries.
    GetCompanyCalendarHolidaysByCompanyCalendarIdAndYear;

public record GetCompanyCalendarHolidaysByCompanyCalendarIdAndYearResponse(Guid Id, DateOnly Date, string Description)
{
    internal class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<CompanyCalendarHoliday, GetCompanyCalendarHolidaysByCompanyCalendarIdAndYearResponse>();
        }
    }
}
