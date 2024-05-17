using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Repositories;

namespace EmployeeControl.Application.Features.CategoryAbsences.Commands.UpdateCategoryAbsence;

internal class UpdateCategoryAbsenceHandler(ICategoryAbsenceRepository categoryAbsenceRepository, IMapper mapper)
    : ICommandHandler<UpdateCategoryAbsenceCommand>
{
    public async Task<Result> Handle(UpdateCategoryAbsenceCommand request, CancellationToken cancellationToken)
    {
        var categoryAbsence = await categoryAbsenceRepository.GetByIdAsync(request.Id, cancellationToken);
        var categoryAbsenceUpdated = mapper.Map(request, categoryAbsence);

        await categoryAbsenceRepository.UpdateAsync(categoryAbsenceUpdated, cancellationToken);

        return Result.Success();
    }
}
