using EmployeeControl.Application.Common.Extensions;
using EmployeeControl.Application.Common.Interfaces;
using EmployeeControl.Application.Common.Models.Settings;
using EmployeeControl.Domain.Constants;
using EmployeeControl.Domain.Entities;
using EmployeeControl.Infrastructure.Data;
using EmployeeControl.Infrastructure.Data.Interceptors;
using EmployeeControl.Infrastructure.Data.Seeds;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EmployeeControl.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Scan(scan =>
            scan.FromCallingAssembly()
                .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Service")))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

        services.AddSingleton(TimeProvider.System);

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        // Interceptors EntityFramework.
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        // Db service.
        services.AddDbContext<ApplicationDbContext>((provider, options) =>
        {
            options.AddInterceptors(provider.GetServices<ISaveChangesInterceptor>());
            options.UseNpgsql(connectionString);
        });

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<ApplicationDbContextInitialize>();

        services.AddAuthorizationBuilder();

        // Identity.
        services
            .AddIdentityCore<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.Configure<IdentityOptions>(options =>
        {
            // Default Password settings.
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 0;
        });

        services.AddAuthorization(options =>
            options.AddPolicy(Policies.CanPurge, policy => policy.RequireRole(Roles.Administrator)));

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var jwtKey = configuration[JwtSettings.JwtKey].SetEmptyIfNull();
                var jwtIssuer = configuration[JwtSettings.JwtIssuer];
                var jwtAudience = configuration[JwtSettings.JwtAudience];

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtIssuer,
                    ValidAudience = jwtAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                    ClockSkew = TimeSpan.Zero
                };
            });

        return services;
    }
}
