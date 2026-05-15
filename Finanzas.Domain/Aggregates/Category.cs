using Finanzas.Domain.ValueObjects;

namespace Finanzas.Domain.Aggregates;

public class Category
{
    public Guid Id { get; private set; }
    public CategoryName Name { get; private set; } = default!;
    public CategoryColor Color { get; private set; } = default!;
    public string Icon { get; private set; } = default!;

    public Category(string name, string color, string icon)
    {
        Id = Guid.NewGuid();
        Update(name, color, icon);
    }

    public void Update(string name, string color, string icon)
    {
        Name = new CategoryName(name);
        Color = new CategoryColor(color);

        if (string.IsNullOrWhiteSpace(icon))
            throw new ArgumentException("El icono no puede estar vacío");

        Icon = icon;
    }
}