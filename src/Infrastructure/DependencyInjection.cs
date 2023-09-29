using Microsoft.Extensions.DependencyInjection;

namespace EmployeeControl.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.Scan(scan =>
            scan.FromCallingAssembly()
                .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Service")))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

        return services;
    }
}
