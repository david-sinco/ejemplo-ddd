using Finanzas.Infrastructure;
using Finanzas.Infrastructure.Persistence;
using Finanzas.Test.Integration.Drivers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reqnroll.Microsoft.Extensions.DependencyInjection;


namespace Finanzas.Test.Integration.Support;

[Binding]
public class Hooks
{
    private static SharedDatabaseFixture _dbFixture = null!;

    [BeforeTestRun]
    public static async Task BeforeTestRun()
    {
        _dbFixture = new SharedDatabaseFixture();
        await _dbFixture.StartContainerAsync();
    }

    [AfterTestRun]
    public static async Task AfterTestRun()
    {
        await _dbFixture.StopContainerAsync();
    }

    [ScenarioDependencies]
    public static IServiceCollection CreateServices()
    {
        var services = new ServiceCollection();

        var baseConnectionString = _dbFixture.Container.GetConnectionString();
        var testConnectionString = $"{baseConnectionString};Database=FinanzasIntegrationTests;TrustServerCertificate=True;";

        var testConfiguration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                { "ConnectionStrings:DefaultConnection", testConnectionString }
            })
            .Build();

        services.AddInfrastructure(testConfiguration);
        services.AddApplication(testConfiguration);

        services.AddScoped<CategoriesDriver>();
        return services;
    }


    [BeforeScenario]
    public static async Task CleanDatabaseBeforeScenario(IServiceProvider scenarioServiceProvider)
    {
        using var scope = scenarioServiceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.Database.MigrateAsync();
    }
}   