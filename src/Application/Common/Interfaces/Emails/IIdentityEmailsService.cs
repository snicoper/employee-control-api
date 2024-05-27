using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Common.Interfaces.Emails;

public interface IIdentityEmailsService
{
    Task SendInviteEmployeeAsync(User user, Company company, string code);

    Task SendRecoveryPasswordAsync(User user, string code);
}
