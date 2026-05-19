namespace Finanzas.Application.Services;

using Finanzas.Application.Dtos;
using Finanzas.Application.Interfaces;
using Finanzas.Domain.Aggregates;
using Finanzas.Domain.Repositories;

public class CategoryAppService(ICategoryRepository repository) : ICategoryAppService
{
    private readonly ICategoryRepository _repository = repository;

    public async Task<CategoryResponse> GetCategoryByNameAsync(string name)
    {
        var category = await _repository.GetByNameAsync(name) ?? throw new KeyNotFoundException("La categoria no existe");
        return new CategoryResponse(category.Id, category.Name, category.Color, category.Icon);
    }

    public async Task CreateCategoryAsync(CreateCategoryRequest request)
    {
        if (await _repository.ExistsAsync(request.Name))
        {
            throw new InvalidOperationException("Ya existe una categoría con ese nombre");
        }

        // 2. Delegación al Dominio (Reglas de Formato/Estado)
        // El constructor de Category disparará las excepciones de los Value Objects si fallan
        var category = new Category(request.Name, request.Color, request.Icon);
        await _repository.AddAsync(category);
    }

    public async Task UpdateCategoryAsync(string currentName, UpdateCategoryRequest request)
    {
        var category = await _repository.GetByNameAsync(currentName) ?? throw new Exception("Categoría no encontrada");

        category.Update(category.Name.Value, request.Color, request.Icon);
        await _repository.UpdateAsync(category);
    }
}