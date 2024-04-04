using AutoMapper;
using EmployeeControl.Domain.Entities;
using MediatR;

namespace EmployeeControl.Application.Features.CompanyHolidays.Command.CreateCompanyHoliday;

public record CreateCompanyHolidayCommand(DateTimeOffset Date, string Description, string CompanyId)
    : IRequest<string>
{
    internal class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<CreateCompanyHolidayCommand, CompanyHoliday>();
        }
    }
}
