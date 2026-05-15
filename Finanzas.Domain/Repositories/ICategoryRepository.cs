using Finanzas.Domain.Aggregates;

namespace Finanzas.Domain.Repositories;

public interface ICategoryRepository
{
    Task<Category?> GetByNameAsync(string name);
    Task AddAsync(Category category);
    Task UpdateAsync(Category category);
    Task<bool> ExistsAsync(string name);
    //Task<int> CountAsync();
}