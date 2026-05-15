using System;
using System.Collections.Generic;
using System.Drawing;
using Finanzas.Application.Interfaces;
using Finanzas.Application.Services;
using Finanzas.Domain.Aggregates;
using Finanzas.Domain.Repositories;
using Finanzas.Domain.ValueObjects;
using Moq;

namespace Finanzas.Test.StepDefinitions;

[Binding]
public class CategoriesSteps
{
    private readonly Mock<ICategoryRepository> _repositoryMock = new();
    private readonly CategoryAppService _appService;
    private string _lastErrorMeessage = default!;

    public CategoriesSteps()
    {
        _appService = new CategoryAppService(_repositoryMock.Object);
    }

    [Given(@"la paleta de colores permitida es: ""(.*)""")]
    public static void GivenLaPaletaDeColoresPermitidaEs(string colores)
    {
        var coloresLista = colores.Split(',').Select(c => c.Trim());
        CategoryColor.SetAllowedColors(coloresLista);
    }

    [Given(@"que ya existe una categoría con los siguientes datos:")]
    public void GivenYaExisteUnaCategoriaConLosSiguientesDatos(DataTable dataTable)
    {
        var row = dataTable.Rows[0];
        string nombre = row["Name"];
        string color = row["Color"];
        string icono = row["Icon"];

        _repositoryMock.Setup(x => x.GetByNameAsync(nombre))
                       .ReturnsAsync(new Category(nombre, color, icono));
        _repositoryMock.Setup(x => x.ExistsAsync(nombre))
                   .ReturnsAsync(true);
    }

    [When(@"creo una categoría con los siguientes datos:")]
    public async Task WhenCreoUnaCategoriaConLosSiguientesDatos(DataTable dataTable)
    {
        try
        {
            var row = dataTable.Rows[0];
            string nombre = row["Name"];
            string color = row["Color"];
            string icono = row["Icon"];

            await _appService.CreateCategoryAsync(nombre, color, icono);
            _repositoryMock.Setup(x => x.GetByNameAsync(nombre))
                       .ReturnsAsync(new Category(nombre, color, icono));
        }
        catch (Exception ex)
        {
            _lastErrorMeessage = ex.Message;
        }
    }

    [When(@"actualizo la categoría con los siguientes datos:")]
    public async Task WhenActualizoLaCategoriaConLosSiguientesDatos(DataTable dataTable)
    {
        try
        {
            var row = dataTable.Rows[0];
            string nombre = row["Name"];
            string color = row["Color"];
            string icono = row["Icon"];

            await _appService.UpdateCategoryAsync(nombre, color, icono);

            _repositoryMock.Setup(x => x.GetByNameAsync(nombre))
                       .ReturnsAsync(new Category(nombre, color, icono));
        }
        catch (Exception ex)
        {
            _lastErrorMeessage = ex.Message;
        }
    }

    [Then(@"la categoría ""(.*)"" debe estar disponible para su uso")]
    public async Task ThenLaCategoriaDebeEstarDisponibleParaSuUso(string nombre)
    {
        _repositoryMock.Verify(
            repo => repo.AddAsync(It.Is<Category>(c => c.Name.Value == nombre)),
            Times.Once,
            "La categoría no fue enviada al repositorio para ser guardada."
        );

        var category = await _appService.GetCategoryByNameAsync(nombre);
        Assert.IsNotNull(category);
        Assert.AreEqual(nombre, category.Name);
    }

    [Then(@"el sistema debe rechazar la creación con el mensaje ""(.*)""")]
    public void ThenElSistemaDebeRechazarLaCreacionConElMensaje(string mensajeEsperado)
    {
        Assert.AreEqual(_lastErrorMeessage, mensajeEsperado);
    }

    [Then(@"la categoría ""(.*)"" debe tener el color ""(.*)""")]
    public async Task ThenLaCategoriaDebeTenerElColor(string nombre, string color)
    {
        _repositoryMock.Verify(
            repo => repo.UpdateAsync(It.Is<Category>(c => c.Name.Value == nombre && c.Color.Value == color)),
            Times.Once,
            "La categoría no fue enviada al repositorio para ser actualizada."
        );

        var category = await _appService.GetCategoryByNameAsync(nombre);
        Assert.IsNotNull(category);
        Assert.AreEqual(nombre, category.Name);
        Assert.AreEqual(color, category.Color);
    }

    [Then(@"la paleta de colores del sistema debe contener exactamente:")]
    public static void ThenLaPaletaDebeTener(DataTable dataTable)
    {
        var coloresEsperados = dataTable.Rows.Select(row => row[0]).ToList();
        var coloresActuales = CategoryColor.AllowedColors;

        // Validamos con MSTest
        Assert.AreEqual(coloresEsperados.Count, coloresActuales.Count, "La cantidad de colores permitidos no coincide.");

        foreach (var color in coloresEsperados)
        {
            Assert.IsTrue(coloresActuales.Contains(color), $"La paleta no contiene el color esperado: {color}");
        }
    }
}