﻿using EmployeeControl.Application.Common.Interfaces.Common;
using EmployeeControl.Application.Common.Interfaces.Features.Company;
using EmployeeControl.Domain.Constants;
using EmployeeControl.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmployeeControl.Infrastructure.Data.Seeds;

public class ApplicationDbContextInitialize(
    ILogger<ApplicationDbContextInitialize> logger,
    ApplicationDbContext context,
    IDateTimeService dateTimeService,
    ICompanyService companyService,
    UserManager<ApplicationUser> userManager,
    RoleManager<ApplicationRole> roleManager)
{
    public async Task InitialiseAsync()
    {
        try
        {
            await context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    private async Task TrySeedAsync()
    {
        await CreateCompanies();
        await CreateRoles();
        await CreateUsers();
    }

    private async Task CreateCompanies()
    {
        var companies = new List<Company> { new() { Name = "Company test" }, new() { Name = "Test Company" } };

        foreach (var company in companies.Where(company => !context.Companies.Any(c => c.Name == company.Name)))
        {
            await companyService.CreateAsync(company, "Europe/Madrid", CancellationToken.None);
        }
    }

    private async Task CreateRoles()
    {
        // Default roles.
        var createRole = new List<ApplicationRole>
        {
            new(Roles.SiteAdmin),
            new(Roles.SiteStaff),
            new(Roles.EnterpriseAdmin),
            new(Roles.EnterpriseStaff),
            new(Roles.HumanResources),
            new(Roles.Employee)
        };

        foreach (var identityRole in createRole.Where(identityRole => roleManager.Roles.All(r => r.Name != identityRole.Name)))
        {
            await roleManager.CreateAsync(identityRole);
        }
    }

    private async Task CreateUsers()
    {
        var companies = context.Companies.ToList();

        // Administrator user.
        var user = new ApplicationUser
        {
            CompanyId = companies[0].Id,
            UserName = "admin@localhost",
            FirstName = "Admin",
            LastName = "Admin1",
            Email = "admin@localhost",
            EntryDate = dateTimeService.UtcNow,
            Active = true,
            EmailConfirmed = true
        };

        if (!await userManager.Users.AnyAsync(u => u.Email == user.Email))
        {
            await userManager.CreateAsync(user, "Password4!");

            var rolesToAdd = new[]
            {
                Roles.SiteAdmin, Roles.SiteStaff, Roles.EnterpriseAdmin, Roles.EnterpriseStaff, Roles.HumanResources,
                Roles.Employee
            };

            await userManager.AddToRolesAsync(user, rolesToAdd);
        }

        // EnterpriseAdministrator user.
        user = new ApplicationUser
        {
            CompanyId = companies[1].Id,
            UserName = "snicoper@gmail.com",
            FirstName = "Salvador",
            LastName = "Nicolas",
            Email = "snicoper@gmail.com",
            EntryDate = dateTimeService.UtcNow,
            Active = true,
            EmailConfirmed = true
        };

        if (!await userManager.Users.AnyAsync(u => u.Email == user.Email))
        {
            await userManager.CreateAsync(user, "Password4!");

            var rolesToAdd = new[] { Roles.EnterpriseAdmin, Roles.EnterpriseStaff, Roles.HumanResources, Roles.Employee };

            await userManager.AddToRolesAsync(user, rolesToAdd);
        }
    }
}
