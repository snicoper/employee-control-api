using EmployeeControl.Application.Common.Extensions;
using EmployeeControl.Application.Common.Interfaces;
using EmployeeControl.Application.Common.Interfaces.Identity;
using EmployeeControl.Domain.Constants;
using EmployeeControl.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace EmployeeControl.Application.Features.Identity.Commands.IdentityRegister;

internal class IdentityRegisterHandler(
        IIdentityService identityService,
        RoleManager<IdentityRole> roleManager,
        IApplicationDbContext context,
        ILogger<IdentityRegisterHandler> logger)
    : IRequestHandler<IdentityRegisterCommand, string>
{
    public async Task<string> Handle(IdentityRegisterCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            // Crear compañía en una transaction.
            var company = new Company { Name = request.CompanyName };
            await context.Company.AddAsync(company, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            var user = new ApplicationUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Active = true,
                CompanyId = company.Id
            };

            var password = request.Password.SetEmptyIfNull();
            var resultResponse = await identityService.CreateUserAsync(user, password);

            // Roles para usuario creado compañia.
            var createRole = new List<IdentityRole>
            {
                new(Roles.EnterpriseAdministrator), new(Roles.Staff), new(Roles.HumanResources), new(Roles.Employee)
            };

            foreach (var identityRole in createRole.Where(identityRole =>
                         roleManager.Roles.All(r => r.Name != identityRole.Name)))
            {
                await roleManager.CreateAsync(identityRole);
            }

            await transaction.CommitAsync(cancellationToken);

            return resultResponse.Id;
        }
        catch (Exception ex)
        {
            logger.LogDebug("{message}", ex.Message);
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
