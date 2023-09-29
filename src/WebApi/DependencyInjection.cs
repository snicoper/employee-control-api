using EmployeeControl.Application.Common.Interfaces;
using EmployeeControl.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using NSwag;
using NSwag.Generation.Processors.Security;

namespace EmployeeControl.WebApi;

public static class DependencyInjection
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services.Scan(scan =>
            scan.FromCallingAssembly()
                .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Service")))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddControllers();
        services.AddEndpointsApiExplorer();

        // Customize default API behavior.
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        // NSwag.
        services.AddOpenApiDocument(configure =>
        {
            configure.Title = "Employee Control API";
            configure.AddSecurity(
                "JWT",
                Enumerable.Empty<string>(),
                new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Type into the text box: Bearer {your JWT token}."
                });

            configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
        });

        // Routing.
        services.AddRouting(options => { options.LowercaseUrls = true; });

        return services;
    }
}
