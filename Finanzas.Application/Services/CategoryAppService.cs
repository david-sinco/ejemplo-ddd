namespace Finanzas.Application.Services;

using Finanzas.Application.Dtos;
using Finanzas.Application.Interfaces;
using Finanzas.Domain.Aggregates;
using Finanzas.Domain.Repositories;

public class CategoryAppService(ICategoryRepository repository, IUserContext userContext) : ICategoryAppService
{
    private readonly ICategoryRepository _repository = repository;
    private readonly IUserContext _userContext = userContext;

    private Guid? UserId => _userContext.UserId;

    public async Task<CategoryResponse> GetCategoryByNameAsync(string name)
    {
        var category = await _repository.GetByNameAndUserAsync(name, UserId) ?? throw new KeyNotFoundException("La categoria no existe");
        return new CategoryResponse(category.Id, category.Name, category.Color, category.Icon);
    }

    public async Task CreateCategoryAsync(CreateCategoryRequest request)
    {
        if (UserId == null)
            throw new ArgumentException("Se necesita el Id del usuario para asociar a la categoria");

        if (await _repository.ExistsByNameAndUserAsync(request.Name, UserId))
        {
            throw new InvalidOperationException("Ya existe una categoría con ese nombre");
        }

        var category = new Category(UserId.Value, request.Name, request.Color, request.Icon);
        await _repository.AddAsync(category);
    }

    public async Task UpdateCategoryAsync(string currentName, UpdateCategoryRequest request)
    {
        if (UserId == null)
            throw new ArgumentException("Se necesita el Id del usuario para asociar a la categoria");

        var category = await _repository.GetByNameAndUserAsync(currentName, UserId) ?? throw new Exception("Categoría no encontrada");

        category.Update(UserId.Value, category.Name.Value, request.Color, request.Icon);
        await _repository.UpdateAsync(category);
    }
}