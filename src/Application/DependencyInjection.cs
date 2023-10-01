using EmployeeControl.Application.Common.Behaviours;
using EmployeeControl.Application.Common.Constants;
using EmployeeControl.Application.Common.Models.Settings;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

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

        // Culture.
        services.Configure<RequestLocalizationOptions>(options =>
        {
            options.DefaultRequestCulture = new RequestCulture(AppCultures.DefaultCulture);
            options.SupportedCultures = AppCultures.GetAll();
            options.SupportedUICultures = AppCultures.GetAll();
        });

        // Localization.
        services.AddLocalization(opt => { opt.ResourcesPath = "Resources"; });

        return services;
    }
}
