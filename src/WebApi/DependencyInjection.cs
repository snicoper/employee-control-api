using EmployeeControl.Application.Common.Interfaces;
using EmployeeControl.WebApi.Services;
using Microsoft.OpenApi.Models;
using System.Reflection;

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

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(
                "v1",
                new OpenApiInfo
                {
                    Version = "v1", Title = "Employee Control", Description = "Web API for managing Employee Control items"
                });

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

        return services;
    }
}
