using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Features.CompanyTask;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Features.CompanyTasks.Commands.CreateCompanyTask;

internal class CreateCompanyTaskHandler(ICompanyTaskService companyTaskService, IMapper mapper)
    : ICommandHandler<CreateCompanyTaskCommand, string>
{
    public async Task<Result<string>> Handle(CreateCompanyTaskCommand request, CancellationToken cancellationToken)
    {
        var newCompanyTask = mapper.Map<CreateCompanyTaskCommand, CompanyTask>(request);
        var companyTask = await companyTaskService.CreateAsync(newCompanyTask, cancellationToken);

        return Result.Success(companyTask.Id);
    }
}
