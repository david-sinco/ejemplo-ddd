using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finanzas.Application.Dtos;
using Finanzas.Application.Interfaces;

namespace Finanzas.Test.Integration.Drivers;

public class AccountDriver(IAccountAppService service)
{
    public async Task ExecuteCrearCuenta(string name, decimal initialBalance, string currency, string icon)
    {
        var request = new CreateAccountRequest(name, initialBalance, currency, icon);
        await service.CreateAccountAsync(request);
    }

    public async Task AssertLaCuentaEstaActiva()
    {

    }
}
