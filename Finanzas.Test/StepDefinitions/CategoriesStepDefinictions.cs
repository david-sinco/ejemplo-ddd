using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finanzas.Test.StepDefinitions;

[Binding]
public class CategoriesStepDefinictions
{
    private string _errorMessage = string.Empty;
    private dynamic _lastCreatedCategory = null;

    [Given(@"la paleta de colores permitida es: ""(.*)""")]
    public void GivenLaPaletaDeColoresPermitidaEs(string colores)
    {
        // Aquí podrías inicializar una constante o validar contra el dominio
        // Por ahora, lo dejamos pendiente (Red)
    }

    [When(@"creo una categoria con los siguientes datos:")]
    public void WhenCreoUnaCategoriaConLosSiguientesDatos(DataTable dataTable)
    {
        // Reqnroll permite mapear tablas a objetos
        var row = dataTable.Rows[0];
        string name = row["Name"];
        string color = row["Color"];
        string icon = row["Icon"];

        // TODO: Llamar a la Capa de Aplicación/Dominio
    }

    [When(@"intento crear una categoria con el nombre valido")]
    public void WhenIntentoCrearUnaCategoriaConElNombreValido(string nombre)
    {
        try
        {
            // TODO: Llamar al constructor de Category en el Dominio
        }
        catch (Exception ex)
        {
            _errorMessage = ex.Message;
        }
    }

    [When(@"intento crear una categoria con el nombre ""(.*)""")]
    public void WhenIntentoCrearUnaCategoriaConElNombre(string nombre)
    {
        try
        {
            // TODO: Llamar al constructor de Category en el Dominio
        }
        catch (Exception ex)
        {
            _errorMessage = ex.Message;
        }
    }

    [Then(@"la categoria ""(.*)"" debe estar disponible para su uso")]
    public void ThenLaCategoriaDebeEstarDisponibleParaSuUso(string nombre)
    {
        // Assert.IsNotNull(_lastCreatedCategory);
        throw new PendingStepException(); // Esto hará que el test aparezca como "Inconclusive"
    }

    [Then(@"el sistema debe rechazar la creacion con el mensaje ""(.*)""")]
    public void ThenElSistemaDebeRechazarLaCreacionConElMensaje(string mensajeEsperado)
    {
        Assert.AreEqual(mensajeEsperado, _errorMessage);
    }
}