using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finanzas.Domain.Aggregates;

namespace Finanzas.Domain.Repositories;

public interface IAccountRepository
{
    Task<bool> ExistsByNameAndUserAsync(string name, Guid? UserId);
    Task<Account?> GetByIdAndUserAsync(Guid id, Guid? userId);
    Task AddAsync(Account account);
    Task UpdateAsync(Account account);
}
