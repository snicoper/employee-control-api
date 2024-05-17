using AutoMapper;
using EmployeeControl.Application.Common.Constants;
using EmployeeControl.Application.Common.Interfaces.Features.Identity;
using EmployeeControl.Application.Common.Interfaces.Messaging;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Application.Common.Utils;
using EmployeeControl.Domain.Constants;
using EmployeeControl.Domain.Entities;
using EmployeeControl.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace EmployeeControl.Application.Features.Employees.Commands.InviteEmployee;

internal class InviteEmployeeHandler(
    IMapper mapper,
    IUserRepository userRepository,
    IIdentityEmailsService identityEmailsService,
    ICompanyRepository companyRepository,
    UserManager<User> userManager,
    IEmployeeSettingsRepository employeeSettingsRepository)
    : ICommandHandler<InviteEmployeeCommand, string>
{
    public async Task<Result<string>> Handle(InviteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var company = await companyRepository.GetCompanyAsync(cancellationToken);
        var user = mapper.Map<InviteEmployeeCommand, User>(request);
        user.CompanyId = company.Id;

        var password = SecurityUtils.GenerateRandomPassword(10);
        var roles = new[] { Roles.Employee };

        // Crear nuevo usuario.
        var (identityResult, newUser) = await userRepository.CreateAsync(user, password, roles, cancellationToken);

        // Configuración de empleado.
        var employeeSettings = new EmployeeSettings { UserId = user.Id, Timezone = request.TimeZone };
        await employeeSettingsRepository.CreateAsync(employeeSettings, cancellationToken);

        // Generar code de validación.
        var code = await userManager.GenerateEmailConfirmationTokenAsync(user);

        // Mandar email de verificación de Email.
        await identityEmailsService.SendInviteEmployeeAsync(user, company, code);

        var resultResponse = identityResult.Succeeded
            ? Result.Success(newUser.Id)
            : Result.Failure<string>(ValidationErrorsKeys.IdentityError, identityResult.Errors.First().Description);

        return resultResponse;
    }
}
