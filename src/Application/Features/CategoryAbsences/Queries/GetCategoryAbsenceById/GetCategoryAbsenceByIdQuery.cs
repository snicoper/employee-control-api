using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;

namespace EmployeeControl.Application.Features.CategoryAbsences.Queries.GetCategoryAbsenceById;

[Authorize(Roles = Roles.HumanResources)]
public record GetCategoryAbsenceByIdQuery(Guid Id) : IQuery<GetCategoryAbsenceByIdResponse>;
