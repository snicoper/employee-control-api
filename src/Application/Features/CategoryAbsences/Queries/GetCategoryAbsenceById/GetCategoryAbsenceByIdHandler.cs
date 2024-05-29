using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Repositories;

namespace EmployeeControl.Application.Features.CategoryAbsences.Queries.GetCategoryAbsenceById;

internal class GetCategoryAbsenceByIdHandler(ICategoryAbsenceRepository categoryAbsenceRepository, IMapper mapper)
    : IQueryHandler<GetCategoryAbsenceByIdQuery, GetCategoryAbsenceByIdResponse>
{
    public async Task<Result<GetCategoryAbsenceByIdResponse>> Handle(
        GetCategoryAbsenceByIdQuery request,
        CancellationToken cancellationToken)
    {
        var categoryAbsence = await categoryAbsenceRepository.GetByIdAsync(request.Id, cancellationToken);
        var resultResponse = mapper.Map<GetCategoryAbsenceByIdResponse>(categoryAbsence);

        return Result.Success(resultResponse);
    }
}
