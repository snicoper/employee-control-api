﻿using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Features;
using EmployeeControl.Application.Common.Interfaces.Features.CompanyTask;
using EmployeeControl.Application.Common.Models;
using MediatR;

namespace EmployeeControl.Application.Features.CompanyTasks.Commands.UpdateCompanyTask;

internal class UpdateCompanyTaskHandler(
    IApplicationDbContext context,
    ICompanyTaskService companyTaskService,
    IEntityValidationService entityValidationService,
    IMapper mapper)
    : IRequestHandler<UpdateCompanyTaskCommand, Result>
{
    public async Task<Result> Handle(UpdateCompanyTaskCommand request, CancellationToken cancellationToken)
    {
        var companyTask = await companyTaskService.GetByIdAsync(request.Id, cancellationToken);

        await entityValidationService.CheckEntityCompanyIsOwner(companyTask);

        var updatedCompanyTask = mapper.Map(request, companyTask);

        context.CompanyTasks.Update(updatedCompanyTask);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
