using AutoMapper;
using EmployeeControl.Application.Common.Constants;
using EmployeeControl.Application.Common.Exceptions;
using EmployeeControl.Application.Common.Interfaces.Data;
using EmployeeControl.Application.Common.Interfaces.Entities.Identity;
using EmployeeControl.Application.Common.Models;
using EmployeeControl.Domain.Constants;
using EmployeeControl.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EmployeeControl.Application.Features.Employees.Commands.InviteEmployee;

internal class InviteEmployeeHandler(
        IMapper mapper,
        IApplicationDbContext context,
        IIdentityService identityService,
        IIdentityEmailsService identityEmailsService,
        UserManager<ApplicationUser> userManager)
    : IRequestHandler<InviteEmployeeCommand, Result>
{
    public async Task<Result> Handle(InviteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var company = await context.Company.SingleOrDefaultAsync(c => c.Id == request.CompanyId, cancellationToken);

        if (company is null)
        {
            throw new NotFoundException(nameof(Company), nameof(Company.Id));
        }

        var user = mapper.Map<InviteEmployeeCommand, ApplicationUser>(request);
        var password = CommonUtils.GenerateRandomPassword(10);
        var roles = new[] { Roles.Employee };

        // Crear nuevo usuario.
        var (result, _) = await identityService.CreateAccountAsync(user, password, roles, cancellationToken);

        // Generar code de validación.
        var code = await userManager.GenerateEmailConfirmationTokenAsync(user);

        // Mandar email de verificación de Email.
        await identityEmailsService.SendInviteEmployeeAsync(user, company, code);

        return result;
    }
}
