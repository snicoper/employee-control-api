using System.Reflection;
using EmployeeControl.Application.Common.Behaviours;
using EmployeeControl.Application.Common.Models.Settings;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeControl.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Scrutor.
        services.Scan(scan =>
            scan.FromCallingAssembly()
                .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Service")))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

        // Automapper.
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        // FluentValidator.
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        // MediatR.
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
            config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
        });

        // Strongly typed options validations.
        services.AddOptions<JwtSettings>()
            .Bind(configuration.GetSection(JwtSettings.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddOptions<EmailSenderSettings>()
            .Bind(configuration.GetSection(EmailSenderSettings.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddOptions<WebAppSettings>()
            .Bind(configuration.GetSection(WebAppSettings.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddOptions<WebApiSettings>()
            .Bind(configuration.GetSection(WebApiSettings.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services;
    }
}
