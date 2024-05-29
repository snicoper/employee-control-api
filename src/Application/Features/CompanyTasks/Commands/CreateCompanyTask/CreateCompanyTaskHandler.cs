using AutoMapper;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Domain.Common;
using EmployeeControl.Domain.Entities;
using EmployeeControl.Domain.Repositories;

namespace EmployeeControl.Application.Features.CompanyTasks.Commands.CreateCompanyTask;

internal class CreateCompanyTaskHandler(ICompanyTaskRepository companyTaskRepository, IMapper mapper)
    : ICommandHandler<CreateCompanyTaskCommand, string>
{
    public async Task<Result<string>> Handle(CreateCompanyTaskCommand request, CancellationToken cancellationToken)
    {
        var newCompanyTask = mapper.Map<CreateCompanyTaskCommand, CompanyTask>(request);
        var companyTask = await companyTaskRepository.CreateAsync(newCompanyTask, cancellationToken);

        return Result.Success(companyTask.Id);
    }
}
