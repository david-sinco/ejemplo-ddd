using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finanzas.Infrastructure.Persistence;
using Finanzas.Test.Integration.Support;
using Microsoft.AspNetCore.Identity;

namespace Finanzas.Test.Integration.StepDefinitions;

[Binding]
public class SharedAuthSteps(FakeUserContext userContext, ApplicationDbContext dbContext, UserManager<IdentityUser<Guid>> userManager)
{
    private readonly FakeUserContext _userContext = userContext;
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly UserManager<IdentityUser<Guid>> _userManager = userManager;

    [Given(@"que existe un usuario con los siguientes datos:")]
    public async Task DadoQueExisteUnUsuarioConLosSiguientesDatos(Table table)
    {
        // Reqnroll lee las filas de la tabla del feature
        var row = table.Rows[0];
        var username = row["UserName"];
        var email = row["Email"];

        // Accedemos al DbContext o UserManager a través de tu fixture de base de datos
        // O si estás usando el ServiceProvider del escenario actual:
        var user = new IdentityUser<Guid>
        {
            UserName = username,
            Email = email,
            NormalizedUserName = username.ToUpperInvariant(),
            NormalizedEmail = email.ToUpperInvariant()
        };

        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }

    [Given(@"que el usuario ""(.*)"" ha iniciado sesión")]
    public async Task DadoQueElUsuarioHaIniciadoSesion(string username)
    {
        var user = await _userManager.FindByEmailAsync(username);
        Assert.IsNotNull(user, "El usuario no existe en base de datos");
        _userContext.UserId = user.Id;
    }
}