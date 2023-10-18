using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Common.Interfaces.Entities.Identity;

public interface IAuthEmailsService
{
    Task SendValidateEmailAsync(ApplicationUser user, Domain.Entities.Company company);
}
