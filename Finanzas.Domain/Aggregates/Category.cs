using Finanzas.Domain.ValueObjects;

namespace Finanzas.Domain.Aggregates;

public class Category
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public CategoryName Name { get; private set; } = default!;
    public CategoryColor Color { get; private set; } = default!;
    public string Icon { get; private set; } = default!;

    // Constructor para EF Core (Shadow constructor)
    private Category() { }

    public Category(Guid userId, string name, string color, string icon)
    {
        Id = Guid.NewGuid();
        Update(userId, name, color, icon);
    }

    public void Update(Guid userId, string name, string color, string icon)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("El usuario es requerido para crear una categoria");

        UserId = userId;
        Name = new CategoryName(name);
        Color = new CategoryColor(color);

        if (string.IsNullOrWhiteSpace(icon))
            throw new ArgumentException("El icono no puede estar vacío");

        Icon = icon;
    }
}