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
public class SharedAuthSteps(FakeUserContext userContext, ApplicationDbContext dbContext)
{
    private readonly FakeUserContext _userContext = userContext;
    private readonly ApplicationDbContext _dbContext = dbContext;

    [Given(@"que existe un usuario con los siguientes datos:")]
    public async Task DadoQueExisteUnUsuarioConLosSiguientesDatos(Table table)
    {
        // Reqnroll lee las filas de la tabla del feature
        var row = table.Rows[0];
        var userId = Guid.Parse(row["Id"]);
        var username = row["UserName"];
        var email = row["Email"];

        // Accedemos al DbContext o UserManager a través de tu fixture de base de datos
        // O si estás usando el ServiceProvider del escenario actual:
        var user = new IdentityUser<Guid>
        {
            Id = userId,
            UserName = username,
            Email = email,
            NormalizedUserName = username.ToUpperInvariant(),
            NormalizedEmail = email.ToUpperInvariant()
        };

        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }

    [Given(@"que el usuario ""(.*)"" con id ""(.*)"" ha iniciado sesión")]
    public void DadoQueElUsuarioHaIniciadoSesion(string username, Guid userId)
    {
        _userContext.UserId = userId;
    }
}