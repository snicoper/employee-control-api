using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Entities;
using EmployeeControl.Domain.Repositories;

namespace EmployeeControl.Application.Features.CategoryAbsences.Commands.CreateCategoryAbsence;

internal class CreateCategoryAbsenceHandler(ICategoryAbsenceRepository categoryAbsenceRepository, IMapper mapper)
    : ICommandHandler<CreateCategoryAbsenceCommand, CategoryAbsence>
{
    public async Task<Result<CategoryAbsence>> Handle(CreateCategoryAbsenceCommand request, CancellationToken cancellationToken)
    {
        var categoryAbsence = mapper.Map<CategoryAbsence>(request);
        await categoryAbsenceRepository.CreateAsync(categoryAbsence, cancellationToken);

        return Result.Success(categoryAbsence);
    }
}
