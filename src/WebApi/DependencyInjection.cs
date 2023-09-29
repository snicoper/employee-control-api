using EmployeeControl.Application.Common.Interfaces;
using EmployeeControl.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using NSwag;
using NSwag.Generation.Processors.Security;
using ZymLabs.NSwag.FluentValidation;

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

        // FluentValidation.
        services.AddScoped(provider =>
        {
            var validationRules = provider.GetService<IEnumerable<FluentValidationRule>>();
            var loggerFactory = provider.GetService<ILoggerFactory>();

            return new FluentValidationSchemaProcessor(provider, validationRules, loggerFactory);
        });

        // API versioning.
        services.AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        });

        services.AddControllersWithViews();
        services.AddEndpointsApiExplorer();
        services.AddRazorPages();

        // NSwag.
        services.AddOpenApiDocument((configure, sp) =>
        {
            configure.Title = "Employee Control API";

            // Add the fluent validations schema processor.
            var fluentValidationSchemaProcessor =
                sp.CreateScope().ServiceProvider.GetRequiredService<FluentValidationSchemaProcessor>();

            configure.SchemaProcessors.Add(fluentValidationSchemaProcessor);

            // Add JWT.
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

        // Customize default API behavior.
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        // Routing.
        services.AddRouting(options => { options.LowercaseUrls = true; });

        return services;
    }
}
