using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Features.CategoryAbsences;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;

namespace EmployeeControl.Application.Features.CategoryAbsences.Commands.UpdateCategoryAbsence;

internal class UpdateCategoryAbsenceHandler(ICategoryAbsenceService categoryAbsenceService, IMapper mapper)
    : ICommandHandler<UpdateCategoryAbsenceCommand>
{
    public async Task<Result> Handle(UpdateCategoryAbsenceCommand request, CancellationToken cancellationToken)
    {
        var categoryAbsence = await categoryAbsenceService.GetByIdAsync(request.Id, cancellationToken);
        var categoryAbsenceUpdated = mapper.Map(request, categoryAbsence);

        await categoryAbsenceService.UpdateAsync(categoryAbsenceUpdated, cancellationToken);

        return Result.Success();
    }
}
