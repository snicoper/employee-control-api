using AutoMapper;
using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Features.CategoryAbsences.Queries.GetCategoryAbsenceById;

public record GetCategoryAbsenceByIdResponse(Guid Id, string Description, string Background, string Color, bool Active)
{
    internal class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<CategoryAbsence, GetCategoryAbsenceByIdResponse>();
        }
    }
}
