using Finanzas.Domain.ValueObjects;

namespace Finanzas.API.Extensions;

internal static class OptionsStartUp
{
    public static IHost InitializeOptions(this IHost app)
    {
        using var scope = app.Services.CreateScope();
        var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

        // Options predefinidos
        var allowedColors = configuration.GetSection("CategorySettings:AllowedColors").Get<string[]>();

        if (allowedColors == null || allowedColors.Length == 0)
        {
            throw new InvalidOperationException("La paleta de colores permitidos no está configurada en appsettings.json");
        }

        // Inicializar el Value Object del Dominio
        CategoryColor.SetAllowedColors(allowedColors);

        return app;
    }
}
