using EmployeeControl.Application;
using EmployeeControl.Domain;
using EmployeeControl.Infrastructure;
using EmployeeControl.WebApi;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddDomainServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddWebApiServices();

builder
    .Host
    .UseSerilog((hostingContext, loggerConfiguration) =>
    {
        loggerConfiguration
            .ReadFrom.Configuration(hostingContext.Configuration)
            .Enrich.FromLogContext()
            .WriteTo.Console(
                outputTemplate:
                "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}",
                theme: AnsiConsoleTheme.Literate);

        if (!hostingContext.HostingEnvironment.IsDevelopment())
        {
            loggerConfiguration.WriteTo.File("web_api_log.txt");
        }
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // NSwag.
    app.UseOpenApi();
    app.UseSwaggerUi3(settings => { settings.Path = string.Empty; });
    app.UseReDoc(settings => { settings.Path = "/docs"; });
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
