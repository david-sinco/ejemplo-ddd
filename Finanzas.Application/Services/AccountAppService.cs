namespace Finanzas.Application.Services;

using Finanzas.Application.Dtos;
using Finanzas.Application.Interfaces;
using Finanzas.Domain.Aggregates;
using Finanzas.Domain.Repositories;
using Finanzas.Domain.ValueObjects;

public class AccountAppService(IAccountRepository repository, IUserContext userContext) : IAccountAppService
{
    private readonly IAccountRepository _repository = repository;
    private readonly IUserContext _userContext = userContext;

    private Guid? UserId => _userContext.UserId;

    public async Task<AccountResponse> GetAccountByIdAsync(Guid id)
    {
        var account = await _repository.GetByIdAndUserAsync(id, UserId)
            ?? throw new KeyNotFoundException("La cuenta no existe");


        return new AccountResponse(
            account.Id,
            account.UserId,
            account.Name,
            account.Balance.Amount,
            account.Balance.Currency,
            account.Icon,
            account.IsActive);
    }

    public async Task CreateAccountAsync(CreateAccountRequest request)
    {
        if (UserId == null)
            throw new ArgumentException("Se necesita el Id del usuario para asociar la cuenta");

        if (await _repository.ExistsByNameAndUserAsync(request.Name, UserId))
        {
            throw new InvalidOperationException("Ya existe una cuenta con el nombre especificado");
        }

        var initialMoney = new Money(request.InitialBalance, request.Currency);
        var account = new Account(UserId.Value, request.Name, initialMoney, request.Icon);

        await _repository.AddAsync(account);
    }

    public async Task CloseAccountAsync(Guid id)
    {
        var account = await _repository.GetByIdAndUserAsync(id, UserId)
            ?? throw new KeyNotFoundException("La cuenta no existe");

        account.Close();
        await _repository.UpdateAsync(account);
    }

    public async Task AdjustBalanceAsync(Guid id, UpdateAccountBalanceRequest request)
    {

        var account = await _repository.GetByIdAndUserAsync(id, UserId)
            ?? throw new KeyNotFoundException("La cuenta no existe");

        account.AdjustBalance(request.NewAmount, request.Reason);
        await _repository.UpdateAsync(account);
    }
}