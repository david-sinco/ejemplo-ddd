using Finanzas.Application.Dtos;
using Finanzas.Application.Interfaces; 
using Finanzas.Domain.Aggregates;
using Finanzas.Domain.Repositories;

namespace Finanzas.Test.Integration.Drivers;

public class CategoriesDriver(ICategoryRepository repository, ICategoryAppService categoryService, IUserContext userContext)
{
    private readonly ICategoryRepository _categoryRepository = repository;
    private readonly ICategoryAppService _categoryService = categoryService;
    private readonly IUserContext _userContext = userContext;

    private Guid? UserId => _userContext.UserId;

    private Exception? _lastException;

    public async Task SetupCategoriaExistenteAsync(string nombre, string color, string icono)
    {
        if(UserId == null) throw new ArgumentNullException(nameof(UserId));

        var category = new Category(UserId.Value, nombre, color, icono);
        await _categoryRepository.AddAsync(category);
    }

    public async Task ExecutePeticionCrearAsync(string nombre, string color, string icono)
    {
        try
        {
            await _categoryService.CreateCategoryAsync(new CreateCategoryRequest(nombre, color, icono));
            _lastException = null;
        }
        catch (Exception ex)
        {
            _lastException = ex;
        }
    }

    public async Task ExecutePeticionActualizarAsync(string nombre, string color, string icono)
    {
        try
        {
            await _categoryService.UpdateCategoryAsync(nombre, new UpdateCategoryRequest(color, icono));
            _lastException = null;
        }
        catch (Exception ex)
        {
            _lastException = ex;
        }
    }

    public async Task AssertCategoriaEstaDisponibleAsync(string nombre)
    {
        var category = await _categoryService.GetCategoryByNameAsync(nombre);
        Assert.IsNotNull(category, $"La categoría '{nombre}' debió guardarse en la DB pero no se encontró.");
        Assert.AreEqual(category.Name, nombre);
    }

    public void AssertMensajeErrorContiene(string mensajeEsperado)
    {
        Assert.IsNotNull(_lastException, "Se esperaba un error de negocio pero la acción fue exitosa.");
        Assert.Contains(_lastException.Message, mensajeEsperado,
            $"El error real '{_lastException.Message}' no contiene el texto esperado: '{mensajeEsperado}'.");
    }

    public async Task VerificarColorActualizadoAsync(string nombre, string color)
    {
        var category = await _categoryService.GetCategoryByNameAsync(nombre);
        Assert.IsNotNull(category, $"La categoría '{nombre}' debió guardarse en la DB pero no se encontró.");
        Assert.AreEqual(category.Name, nombre);
        Assert.AreEqual(category.Color, color);
    }
}
