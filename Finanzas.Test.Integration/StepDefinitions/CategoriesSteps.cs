using Reqnroll;
using Finanzas.Test.Integration.Drivers;

namespace Finanzas.Test.Integration.StepDefinitions;

[Binding]
public class CategoriesSteps(CategoriesDriver driver)
{
    private readonly CategoriesDriver _driver = driver;

    [Given(@"la paleta de colores permitida es: ""(.*)""")]
    public static void GivenLaPaletaDeColoresPermitidaEs(string colores)
    {
        Domain.ValueObjects.CategoryColor.SetAllowedColors(colores.Split(',').Select(c => c.Trim()));
    }

    [Given(@"que ya existe una categoría con los siguientes datos:")]
    public async Task GivenQueYaExisteUnaCategoriaConLosSiguientesDatos(DataTable dataTable)
    {
        var row = dataTable.Rows[0];
        await _driver.SetupCategoriaExistenteAsync(row["Name"], row["Color"], row["Icon"]);
    }

    [When(@"creo una categoría con los siguientes datos:")]
    public async Task WhenCreoUnaCategoriaConLosSiguientesDatos(DataTable dataTable)
    {
        var row = dataTable.Rows[0];
        await _driver.ExecutePeticionCrearAsync(row["Name"], row["Color"], row["Icon"]);
    }

    [When(@"actualizo la categoría con los siguientes datos:")]
    public async Task WhenActualizoLaCategoriaConLosSiguientesDatos(DataTable dataTable)
    {
        var row = dataTable.Rows[0];
        await _driver.ExecutePeticionActualizarAsync(row["Name"], row["Color"], row["Icon"]);
    }

    [Then(@"la categoría ""(.*)"" debe estar disponible para su uso")]
    public async Task ThenLaCategoriaDebeEstarDisponibleParaSuUso(string nombre) => await _driver.AssertCategoriaEstaDisponibleAsync(nombre);

    [Then(@"el sistema debe rechazar la creación con el mensaje ""(.*)""")]
    public void ThenElSistemaDebeRechazarLaCreacionConElMensaje(string mensaje) => _driver.AssertMensajeErrorContiene(mensaje);

    [Then(@"la categoría ""(.*)"" debe tener el color ""(.*)""")]
    public async Task ThenLaCategoriaDebeTenerElColor(string nombre, string color) => await _driver.VerificarColorActualizadoAsync(nombre, color);

    [Then(@"la paleta de colores del sistema debe contener exactamente:")]
    public static void ThenLaPaletaDeColoresDelSistemaDebeContenerExactamente(DataTable dataTable)
    {
        var colores = dataTable.Rows.Select(r => r[0]).ToList();
        var coloresActuales = Domain.ValueObjects.CategoryColor.AllowedColors;
        Assert.AreEqual(colores.Count, coloresActuales.Count);
    }
}