using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Common.Interfaces.Entities.Identity;

public interface IIdentityEmailsService
{
    Task SendValidateEmailAsync(ApplicationUser user, Domain.Entities.Company company, string code);

    Task SendRecoveryPasswordAsync(ApplicationUser user, string code);
}
