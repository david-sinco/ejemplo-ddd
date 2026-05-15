namespace Finanzas.Domain.ValueObjects;

public record CategoryColor
{
    private static string[] _allowedColors = [];
    public static IReadOnlyCollection<string> AllowedColors => _allowedColors;


    public static void SetAllowedColors(IEnumerable<string> colors)
    {
        if (_allowedColors.Length > 0) return;
        _allowedColors = [.. colors];
    }


    public string Value { get; }

    public CategoryColor(string value)
    {
        if (!_allowedColors.Contains(value))
            throw new ArgumentException("El color no pertenece a la paleta permitida");

        Value = value;
    }

    public static implicit operator string(CategoryColor color) => color.Value;
}