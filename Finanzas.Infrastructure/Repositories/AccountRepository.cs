using Finanzas.Domain.Aggregates;
using Finanzas.Domain.Repositories;
using Finanzas.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Finanzas.Infrastructure.Repositories;

internal class AccountRepository(ApplicationDbContext context) : IAccountRepository
{
    private readonly ApplicationDbContext _context = context;


    public async Task<Account?> GetByNameAndUserAsync(string name, Guid? userId)
    {
        return await _context.Accounts
            .FirstOrDefaultAsync(a => a.Name == name && a.UserId == userId);
    }

    public async Task<bool> ExistsByNameAndUserAsync(string name, Guid? userId)
    {
        return await _context.Accounts
            .AnyAsync(a => a.Name == name && a.UserId == userId);
    }

    public async Task<Account?> GetByIdAndUserAsync(Guid id, Guid? userId)
    {
        return await _context.Accounts
            .FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);
    }

    public async Task<bool> ExistsByIdAndUserAsync(Guid id, Guid? userId)
    {
        return await _context.Accounts
            .AnyAsync(a => a.Id == id && a.UserId == userId);
    }

    public async Task<Account?> GetByNameAndUserAsync(string name, Guid userId)
    {
        return await _context.Accounts
            .FirstOrDefaultAsync(a => a.Name == name && a.UserId == userId);
    }

    public async Task AddAsync(Account account)
    {
        await _context.Accounts.AddAsync(account);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Account account)
    {
        _context.Accounts.Update(account);
        await _context.SaveChangesAsync();
    }
}