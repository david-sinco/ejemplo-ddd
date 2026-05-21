using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finanzas.Application.Dtos;

namespace Finanzas.Application.Interfaces;

public interface IAccountAppService
{
    public Task<AccountResponse> GetAccountByNameAndUserId(string name, Guid? userId);
    public Task<AccountResponse> GetAccountByIdAsync(Guid id);
    public Task CreateAccountAsync(CreateAccountRequest request);
    public Task CloseAccountAsync(Guid id);
    public Task AdjustBalanceAsync(Guid id, UpdateAccountBalanceRequest request);
}
