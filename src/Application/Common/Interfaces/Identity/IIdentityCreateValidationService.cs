using EmployeeControl.Domain.Entities;

namespace EmployeeControl.Application.Common.Interfaces.Identity;

public interface IIdentityCreateValidationService
{
    Task UserValidationAsync(ApplicationUser applicationUser);

    Task PasswordValidationAsync(ApplicationUser applicationUser, string password);
}
