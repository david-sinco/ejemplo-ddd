using Testcontainers.MsSql;

namespace Finanzas.Test.Integration.Support;

public class SharedDatabaseFixture
{
    // Cambiamos al contenedor específico de MS SQL
    public MsSqlContainer Container { get; private set; } = null!;

    public async Task StartContainerAsync()
    {
        Container = new MsSqlBuilder("mcr.microsoft.com/mssql/server:2022-latest")
            .WithPassword("PasswordSeguro123!*")
            .WithPortBinding("5555", "1433")
            .WithName("test-container-sql")
            .Build();

        await Container.StartAsync();
    }

    public async Task StopContainerAsync()
    {
        if (Container != null)
        {
            await Container.StopAsync();
            await Container.DisposeAsync();
        }
    }
}