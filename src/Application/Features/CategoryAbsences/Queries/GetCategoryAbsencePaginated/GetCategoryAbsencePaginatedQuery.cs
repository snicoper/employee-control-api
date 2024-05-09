using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Application.Common.Security;
using EmployeeControl.Domain.Constants;

namespace EmployeeControl.Application.Features.CategoryAbsences.Queries.GetCategoryAbsencePaginated;

[Authorize(Roles = Roles.HumanResources)]
public record GetCategoryAbsencePaginatedQuery(RequestData RequestData)
    : IQuery<ResponseData<GetCategoryAbsencePaginatedResponse>>;
