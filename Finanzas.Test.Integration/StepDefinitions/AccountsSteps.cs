using Finanzas.Test.Integration.Drivers;

namespace Finanzas.Test.Integration.StepDefinitions;

[Binding]
public class AccountsSteps(AccountDriver driver)
{
    private readonly AccountDriver _driver = driver;

    [Given(@"que no existe una cuenta llamada ""(.*)""")]
#pragma warning disable IDE0060 // Remove unused parameter
    public static void GivenLaPaletaDeColoresPermitidaEs(string cuenta)
#pragma warning restore IDE0060 // Remove unused parameter
    {
        //No es necesario hacer nada ya que no hay datos en el set de pruebas, no existe la cuenta
    }

    [When(@"abro una cuenta con los siguientes datos:")]
    public async Task WhenAbroUnaCuentaConDatos(DataTable data)
    {
        var row = data.Rows[0];
        var nombre = row["Nombre"];
        var saldoInicial = decimal.Parse(row["Saldo Inicial"]);
        var moneda = row["Moneda"];
        var icono = row["Icono"];

        await _driver.ExecuteCrearCuenta(nombre, saldoInicial, moneda, icono);
    }

    [Then(@"la cuenta ""(.*)"" debe estar activa")]
    public async Task ThenLaCuentaDebeEstarActiva(string cuenta)
        => await _driver.AssertLaCuentaEstaActiva(cuenta);

    [Then(@"la cuenta ""(.*)"" debe tener un saldo de ""(.*)"" ""(.*)""")]
    public async Task ThenElSaldoActualDebeSer(string cuenta, decimal saldo, string currency)
        => await _driver.AssertElsaldoDebeSer(cuenta, saldo, currency);

    [Then(@"la cuenta ""(.*)"" debe tener un saldo inicial de ""(.*)"" ""(.*)""")]
    public async Task ThenElSaldoInicialDebeSer(string cuenta, decimal saldo, string currency)
        => await _driver.AssertElSaldoInicialDebeSer(cuenta, saldo, currency);

    [Then(@"el id del usuario de la cuenta ""(.*)"" debe pertenecer a ""(.*)""")]
    public async Task ThenElIdDebeSer(string cuenta, string correo)
        => await _driver.AssertLaCuentaPerteneceA(cuenta, correo);
}
