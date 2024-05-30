using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Common.Interfaces.Emails;

public interface ICompanyTaskEmailsService
{
    Task SendEmployeeAssignTaskAsync(
        CompanyTask companyTask,
        Company company,
        List<User> users);
}
