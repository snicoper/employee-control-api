using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Features.CategoryAbsences;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Features.CategoryAbsences.Commands.CreateCategoryAbsence;

internal class CreateCategoryAbsenceHandler(ICategoryAbsenceService categoryAbsenceService, IMapper mapper)
    : ICommandHandler<CreateCategoryAbsenceCommand, CategoryAbsence>
{
    public async Task<Result<CategoryAbsence>> Handle(CreateCategoryAbsenceCommand request, CancellationToken cancellationToken)
    {
        var categoryAbsence = mapper.Map<CategoryAbsence>(request);
        await categoryAbsenceService.CreateAsync(categoryAbsence, cancellationToken);

        return Result.Success(categoryAbsence);
    }
}
