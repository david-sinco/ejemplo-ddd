using Finanzas.Domain.Aggregates;

namespace Finanzas.Application.Interfaces;

public interface ICategoryAppService
{
    Task<Category> GetCategoryByNameAsync(string name);
    Task CreateCategoryAsync(string name, string color, string icon);
    Task UpdateCategoryAsync(string currentName, string newColor, string icon);
}