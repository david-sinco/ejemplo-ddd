using Finanzas.API;
using Finanzas.API.Extensions;
using Finanzas.Application.Interfaces;
using Finanzas.Infrastructure;
using Finanzas.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource.AddService("finanzas-api"))
    .WithMetrics(metrics =>
    {
        metrics
            .AddAspNetCoreInstrumentation() // Métricas nativas de HTTP (peticiones, respuestas, etc.)
            .AddRuntimeInstrumentation()    // Métricas del CLR (Uso de CPU, memoria, Garbage Collector)
            .AddPrometheusExporter();       // Expone los datos en formato Prometheus
    });

builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<IdentityUser<Guid>>()
    .AddRoles<IdentityRole<Guid>>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IUserContext, HttpUserContext>();

var app = builder.Build();

//Ejecutar migraciones solo en dev
if (app.Environment.IsDevelopment())
{
    try
    {
        using IServiceScope scope = app.Services.CreateScope();
        using ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await context.Database.MigrateAsync();
    }
    catch (Exception ex)
    {
        var logger = app.Services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Error al aplicar las migraciones en el inicio.");
    }
}

app.MapGroup("/session")
    .MapIdentityApi<IdentityUser<Guid>>();
app.MapPrometheusScrapingEndpoint();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseWhen(context => !context.Request.Path.StartsWithSegments("/metrics"), appBuilder =>
{
    appBuilder.UseHttpsRedirection();
});
//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.LoadOptions();

app.MapControllers();

app.Run();
