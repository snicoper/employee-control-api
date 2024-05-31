using System.Reflection;
using EmployeeControl.Application.Common.Constants;
using EmployeeControl.Application.Common.Interfaces.Users;
using EmployeeControl.Application.Common.Localization;
using EmployeeControl.WebApi.Infrastructure;
using EmployeeControl.WebApi.Services.Users;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;

namespace EmployeeControl.WebApi;

public static class DependencyInjection
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services)
    {
        services.AddDatabaseDeveloperPageExceptionFilter();
        services.AddHttpContextAccessor();

        services.Scan(
            scan =>
                scan.FromCallingAssembly()
                    .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Service")))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());

        services.AddScoped<ICurrentUserService, CurrentUserService>();

        // API versioning.
        services.AddApiVersioning(
            options =>
            {
                options.ReportApiVersions = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            });

        services.AddExceptionHandler<CustomExceptionHandler>();

        services.AddControllersWithViews()
            .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
            .AddDataAnnotationsLocalization(
                options => { options.DataAnnotationLocalizerProvider = (_, factory) => factory.Create(typeof(SharedResource)); });

        // Customize default API behavior.º
        services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

        // Routing.
        services.AddRouting(options => { options.LowercaseUrls = true; });

        // OpenApi.
        services.AddSwaggerGen(
            options =>
            {
                options.SwaggerDoc(
                    "v1",
                    new OpenApiInfo { Version = "v1", Title = "Employee Control API", Description = "An ASP.NET Core Web API" });

                options.AddSecurityDefinition(
                    "Bearer",
                    new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Please insert JWT with Bearer into field",
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey
                    });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

        // Cors.
        services.AddCors(
            options =>
            {
                options.AddPolicy(
                    "DefaultCors",
                    builder =>
                    {
                        builder
                            .WithOrigins("http://localhost:4200")
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                    });
            });

        services.Configure<RazorViewEngineOptions>(
            options =>
            {
                options.ViewLocationFormats.Clear();
                options.ViewLocationFormats.Add("/Views/{1}/{0}" + RazorViewEngine.ViewExtension);
                options.ViewLocationFormats.Add("/Views/Emails/{0}" + RazorViewEngine.ViewExtension);
                options.ViewLocationFormats.Add("/Views/Shared/{0}" + RazorViewEngine.ViewExtension);
            });

        // Culture.
        services.Configure<RequestLocalizationOptions>(
            options =>
            {
                options.DefaultRequestCulture = new RequestCulture(AppCultures.DefaultCulture, AppCultures.DefaultCulture);
                options.SupportedCultures = AppCultures.GetAll();
                options.SupportedUICultures = AppCultures.GetAll();
            });

        // Localization.
        services.AddLocalization(options => { options.ResourcesPath = "Common/Resources"; });

        return services;
    }
}
