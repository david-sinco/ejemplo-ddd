using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Docker.DotNet.Models;
using Finanzas.Test.Integration.Drivers;

namespace Finanzas.Test.Integration.StepDefinitions;

[Binding]
public class AccountsSteps(AccountDriver driver)
{
    private readonly AccountDriver _driver = driver;

    [Given(@"que no existe una cuenta llamada ""(.*)""")]
    public static void GivenLaPaletaDeColoresPermitidaEs(string cuenta)
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

    [Then(@"la cuenta ""*"" debe estar activa")]
    public async Task ThenLaCuentaDebeEstarActiva(string cuenta)
    {

    }
}
