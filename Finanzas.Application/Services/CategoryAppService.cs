namespace Finanzas.Application.Services;

using Finanzas.Application.Interfaces;
using Finanzas.Domain.Aggregates;
using Finanzas.Domain.Repositories;

public class CategoryAppService(ICategoryRepository repository) : ICategoryAppService
{
    private readonly ICategoryRepository _repository = repository;

    public async Task<Category> GetCategoryByNameAsync(string name)
    {
        var category = await _repository.GetByNameAsync(name) ?? throw new KeyNotFoundException("La categoria no existe");
        return category;
    }

    public async Task CreateCategoryAsync(string name, string color, string icon)
    {
        if (await _repository.ExistsAsync(name))
        {
            throw new InvalidOperationException("Ya existe una categoría con ese nombre");
        }

        // 2. Delegación al Dominio (Reglas de Formato/Estado)
        // El constructor de Category disparará las excepciones de los Value Objects si fallan
        var category = new Category(name, color, icon);
        await _repository.AddAsync(category);
    }

    public async Task UpdateCategoryAsync(string currentName, string newColor, string icon)
    {
        var category = await _repository.GetByNameAsync(currentName) ?? throw new Exception("Categoría no encontrada");

        // Usamos el método Update de tu entidad
        category.Update(category.Name.Value, newColor, icon);

        await _repository.UpdateAsync(category);
    }
}