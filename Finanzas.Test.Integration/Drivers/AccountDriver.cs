using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finanzas.Application.Dtos;
using Finanzas.Application.Interfaces;
using Finanzas.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Finanzas.Test.Integration.Drivers;

public class AccountDriver(IAccountAppService service, IUserContext userContext, IAccountRepository repository, UserManager<IdentityUser<Guid>> userManager)
{
    private readonly IAccountAppService _service = service;
    private readonly IAccountRepository _repository = repository;
    private readonly IUserContext _userContext = userContext;
    private readonly UserManager<IdentityUser<Guid>> _userManager = userManager;

    public async Task ExecuteCrearCuenta(string name, decimal initialBalance, string currency, string icon)
    {
        var request = new CreateAccountRequest(name, initialBalance, currency, icon);
        await _service.CreateAccountAsync(request);
    }

    public async Task AssertLaCuentaEstaActiva(string name)
    {
        var account = await _service.GetAccountByNameAndUserId(name, _userContext.UserId);
        Assert.IsTrue(account.IsActive);
    }

    public async Task AssertElsaldoDebeSer(string name, decimal saldo, string currency)
    {
        var account = await _service.GetAccountByNameAndUserId(name, _userContext.UserId);
        Assert.AreEqual(account.Balance, saldo);
        Assert.AreEqual(account.Currency, currency);
    }

    public async Task AssertElSaldoInicialDebeSer(string name, decimal saldo, string currency)
    {
        var account = await _repository.GetByNameAndUserAsync(name, _userContext.UserId);
        Assert.IsNotNull(account);
        Assert.AreEqual(account.InitialBalance.Amount, saldo);
        Assert.AreEqual(account.InitialBalance.Currency, currency);
    }

    public async Task AssertLaCuentaPerteneceA(string name, string email)
    {
        var account = await _repository.GetByNameAndUserAsync(name, _userContext.UserId);
        Assert.IsNotNull(account);

        var user = await _userManager.FindByIdAsync(account.UserId.ToString());
        Assert.IsNotNull(user);
        Assert.AreEqual(user.Email, email);
    }
}
