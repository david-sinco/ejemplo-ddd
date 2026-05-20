using Finanzas.Domain.Aggregates;
using Finanzas.Test.Unit.Drivers;

namespace Finanzas.Test.Unit.Domain;

[TestClass]
public class Categorias
{
    private CategoriesTestDriver _driver = null!;

    [TestInitialize]
    public void Setup()
    {
        _driver = new CategoriesTestDriver();
    }

    [TestMethod]
    public async Task CrearCategoria_ConDatosValidos_DeberiaGuardarEnRepositorio()
    {
        await _driver
            .ConPaletaDeColores("Rojo, Azul, Verde")
            .EjecutarCrearCategoriaAsync(Guid.NewGuid(), "Educación", "Azul", "book-icon");

        // Assert
        await _driver.VerificarCategoriaCreadaExitosamenteAsync("Educación");
    }

    [TestMethod]
    public void CrearCategoria_ConNombreDemasiadoCorto_DebeLanzarExcepcion()
    {
        var nombreCorto = "Yo";

        Assert.ThrowsException<DomainException>(() =>
        {
            new Category(Guid.NewGuid(), nombreCorto, "Verde", "fast-food");
        });
    }
}
