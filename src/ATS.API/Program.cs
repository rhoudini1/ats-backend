using ATS.API.Filters;
using ATS.API.Health;
using ATS.Application;
using ATS.Infrastructure;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Scalar.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

try
{
    Log.Information("Starting web application");

    builder.Host.UseSerilog();

    builder.Services.AddControllers();

    builder.Services.AddHealthChecks()
        .AddCheck<DatabaseHealthCheck>(DatabaseHealthCheck.Name, tags: ["db"], timeout: TimeSpan.FromSeconds(5));

    builder.Services.AddOpenApi();

    builder.Services.AddMvc(options => options.Filters.Add<ExceptionFilter>());

    builder.Services.AddInfrastructure(builder.Configuration);

    builder.Services.AddApplication();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
        app.MapScalarApiReference();
    }

    app.MapHealthChecks("_health", new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

    // Logs all HTTP requests. Could be turned off later.
    app.UseSerilogRequestLogging();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly.");
}
finally
{
    Log.CloseAndFlush();
}
