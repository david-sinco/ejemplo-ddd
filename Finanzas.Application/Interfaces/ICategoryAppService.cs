using Finanzas.Application.Dtos;
using Finanzas.Domain.Aggregates;

namespace Finanzas.Application.Interfaces;

public interface ICategoryAppService
{
    Task<CategoryResponse> GetCategoryByNameAsync(string name);
    Task CreateCategoryAsync(CreateCategoryRequest request);
    Task UpdateCategoryAsync(string currentName, UpdateCategoryRequest request);
}