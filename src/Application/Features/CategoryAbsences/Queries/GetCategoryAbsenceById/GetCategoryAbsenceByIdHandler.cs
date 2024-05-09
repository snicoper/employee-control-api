using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Features.CategoryAbsences;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;

namespace EmployeeControl.Application.Features.CategoryAbsences.Queries.GetCategoryAbsenceById;

internal class GetCategoryAbsenceByIdHandler(ICategoryAbsenceService categoryAbsenceService, IMapper mapper)
    : IQueryHandler<GetCategoryAbsenceByIdQuery, GetCategoryAbsenceByIdResponse>
{
    public async Task<Result<GetCategoryAbsenceByIdResponse>> Handle(
        GetCategoryAbsenceByIdQuery request,
        CancellationToken cancellationToken)
    {
        var categoryAbsence = await categoryAbsenceService.GetByIdAsync(request.Id, cancellationToken);
        var resultResponse = mapper.Map<GetCategoryAbsenceByIdResponse>(categoryAbsence);

        return Result.Success(resultResponse);
    }
}
