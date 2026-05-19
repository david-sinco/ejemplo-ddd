using Finanzas.Application.Dtos;
using Finanzas.Application.Services;
using Finanzas.Domain.Aggregates;
using Finanzas.Domain.Repositories;
using Finanzas.Domain.ValueObjects;
using Moq;

namespace Finanzas.Test.Unit.Drivers;

public class CategoriesTestDriver
{
    private readonly Mock<ICategoryRepository> _repositoryMock = new();
    private readonly CategoryAppService _appService;
    private string? _lastErrorMessage;

    public CategoriesTestDriver()
    {
        _appService = new CategoryAppService(_repositoryMock.Object);
    }

    public CategoriesTestDriver ConPaletaDeColores(string colores)
    {
        var coloresLista = colores.Split(',').Select(c => c.Trim());
        CategoryColor.SetAllowedColors(coloresLista);
        return this;
    }

    public CategoriesTestDriver ConCategoriaExistente(string nombre, string color, string icono)
    {
        _repositoryMock.Setup(x => x.GetByNameAsync(nombre))
                       .ReturnsAsync(new Category(nombre, color, icono));

        _repositoryMock.Setup(x => x.ExistsAsync(nombre))
                       .ReturnsAsync(true);
        return this;
    }

    public async Task<CategoriesTestDriver> EjecutarCrearCategoriaAsync(string nombre, string color, string icono)
    {
        try
        {
            await _appService.CreateCategoryAsync(new CreateCategoryRequest(nombre, color, icono));

            _repositoryMock.Setup(x => x.GetByNameAsync(nombre))
                           .ReturnsAsync(new Category(nombre, color, icono));
        }
        catch (Exception ex)
        {
            _lastErrorMessage = ex.Message;
        }
        return this;
    }

    public async Task<CategoriesTestDriver> EjecutarActualizarCategoriaAsync(string nombre, string color, string icono)
    {
        try
        {
            await _appService.UpdateCategoryAsync(nombre, new UpdateCategoryRequest(color, icono));

            _repositoryMock.Setup(x => x.GetByNameAsync(nombre))
                           .ReturnsAsync(new Category(nombre, color, icono));
        }
        catch (Exception ex)
        {
            _lastErrorMessage = ex.Message;
        }
        return this;
    }

    public async Task VerificarCategoriaCreadaExitosamenteAsync(string nombre)
    {
        _repositoryMock.Verify(
            repo => repo.AddAsync(It.Is<Category>(c => c.Name.Value == nombre)),
            Times.Once,
            "La categoría no fue enviada al repositorio."
        );

        var category = await _appService.GetCategoryByNameAsync(nombre);
        Assert.IsNotNull(category, "La categoría debería existir tras crearse.");
        Assert.AreEqual(nombre, category.Name);
    }

    public async Task VerificarActualizacionDeColorExitosoAsync(string nombre, string color)
    {
        _repositoryMock.Verify(
            repo => repo.UpdateAsync(It.Is<Category>(c => c.Name.Value == nombre && c.Color.Value == color)),
            Times.Once,
            "La categoría no fue enviada al repositorio para actualizarse."
        );

        var category = await _appService.GetCategoryByNameAsync(nombre);
        Assert.IsNotNull(category);
        Assert.AreEqual(color, category.Color);
    }

    public void VerificarMensajeError(string mensajeEsperado)
    {
        Assert.AreEqual(mensajeEsperado, _lastErrorMessage, "El mensaje de error no coincide.");
    }

    public void VerificarPaletaColores(List<string> coloresEsperados)
    {
        var coloresActuales = CategoryColor.AllowedColors;
        Assert.AreEqual(coloresEsperados.Count, coloresActuales.Count);
        foreach (var color in coloresEsperados)
        {
            Assert.IsTrue(coloresActuales.Contains(color), $"Falta el color esperado en la paleta: {color}");
        }
    }
}
