namespace Finanzas.Domain.ValueObjects;

public record CategoryName
{
    public string Value { get; }

    public CategoryName(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length < 3)
            throw new ArgumentException("El nombre debe tener al menos 3 caracteres");

        Value = value.Trim();
    }

    public static implicit operator string(CategoryName name) => name.Value;
}