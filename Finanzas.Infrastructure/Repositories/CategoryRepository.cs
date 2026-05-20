using Finanzas.Domain.Aggregates;
using Finanzas.Domain.Repositories;
using Finanzas.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Finanzas.Infrastructure.Repositories;

internal class CategoryRepository(ApplicationDbContext context) : ICategoryRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Category?> GetByNameAndUserAsync(string name, Guid? userId)
    {
        return await _context.Categories
            .FirstOrDefaultAsync(c => (string)c.Name == name && c.UserId == userId);
    }

    public async Task AddAsync(Category category)
    {
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Category category)
    {
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsByNameAndUserAsync(string name, Guid? userId)
    {
        return await _context.Categories
            .AnyAsync(c => (string)c.Name == name && c.UserId == userId);
    }
}