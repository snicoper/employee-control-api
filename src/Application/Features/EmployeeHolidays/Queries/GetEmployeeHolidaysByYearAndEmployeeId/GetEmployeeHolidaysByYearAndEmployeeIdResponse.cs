using AutoMapper;
using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Features.EmployeeHolidays.Queries.GetEmployeeHolidaysByYearAndEmployeeId;

public record GetEmployeeHolidaysByYearAndEmployeeIdResponse(string Id, int Year, int TotalDays, int Consumed, string UserId)
{
    internal class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<EmployeeHoliday, GetEmployeeHolidaysByYearAndEmployeeIdResponse>();
        }
    }
}
