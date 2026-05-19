using Finanzas.Domain.ValueObjects;

namespace Finanzas.API.Extensions;


// Esta clae se usa para configurar el patron options en todo el proyecto
// Se usa para configurar y mapear configuraciones del appsettings directamente a una clase que se pueda inyectar
// Tembien se puede usar para ajustar o parametrizar la aplicación con datos de inicio que se cargan desde el appsettings
internal static class Options
{
    public static IHost LoadOptions(this IHost app)
    {
        using var scope = app.Services.CreateScope();
        var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();


        var allowedColors = configuration.GetSection("CategorySettings:AllowedColors").Get<string[]>();
        if (allowedColors == null || allowedColors.Length == 0)
        {
            throw new InvalidOperationException("La paleta de colores permitidos no está configurada en appsettings.json");
        }
        CategoryColor.SetAllowedColors(allowedColors);


        return app;
    }
}
