using EmployeeControl.Application;
using EmployeeControl.Application.Common.Interfaces.BackgroundJobs;
using EmployeeControl.Application.Common.Services.Hubs;
using EmployeeControl.Infrastructure;
using EmployeeControl.Infrastructure.Data.Seeds;
using EmployeeControl.WebApi;
using Hangfire;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration, builder.Environment)
    .AddWebApiServices();

builder.Host.UseSerilog(
    (context, configuration) =>
        configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

// Initialise Database in development.
if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();

    app.UseSwagger();
    app.UseSwaggerUI(
        options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            options.RoutePrefix = string.Empty;
        });
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseSerilogRequestLogging();

app.UseRequestLocalization();

app.UseExceptionHandler(_ => { });

app.UseCors("DefaultCors");

app.UseAuthentication();
app.UseAuthorization();

// Hangfire.
app.UseHangfireDashboard();
RecurringJob.AddOrUpdate<ICloseTimeControlJob>("close-time-control-job", service => service.Process(), "*/30 * * * *");

app.MapControllers();

// Configure our SignalR hub
app.MapHub<NotificationTimeControlIncidenceHub>("hub");

app.Run();
