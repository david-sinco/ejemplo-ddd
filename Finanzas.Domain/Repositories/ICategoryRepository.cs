using Finanzas.Domain.Aggregates;

namespace Finanzas.Domain.Repositories;

public interface ICategoryRepository
{
    Task<Category?> GetByNameAndUserAsync(string name, Guid? userId);
    Task AddAsync(Category category);
    Task UpdateAsync(Category category);
    Task<bool> ExistsByNameAndUserAsync(string name, Guid? userId);
    //Task<int> CountAsync();
}