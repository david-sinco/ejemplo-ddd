using Finanzas.Domain.Entities;
using Finanzas.Domain.ValueObjects;

namespace Finanzas.Domain.Aggregates;

public class Account
{
    public Guid Id { get; private set; } = default!;
    public Guid UserId { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public Money Balance { get; private set; } = default!;
    public Money InitialBalance { get; private set; } = default!;
    public string Icon { get; private set; } = default!;
    public bool IsActive { get; private set; }

    private readonly List<Movement> _movements = [];
    public IReadOnlyCollection<Movement> Movements => _movements.AsReadOnly();

    // Constructor requerido por EF Core
    private Account() { }

    public Account(Guid userId, string name, Money initialBalance, string icon)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("El usuario es requerido para crear una cuenta.");

        if (string.IsNullOrWhiteSpace(name) || name.Length < 3)
            throw new ArgumentException("El nombre de la cuenta debe tener al menos 3 caracteres");

        Id = Guid.NewGuid();
        UserId = userId;
        Name = name;
        Balance = initialBalance;
        InitialBalance = initialBalance;
        Icon = icon;
        IsActive = true;
    }

    public void Close()
    {
        if (Balance.Amount > 0)
            throw new InvalidOperationException("No se puede cerrar una cuenta con saldo mayor a cero");

        IsActive = false;
    }

    public void AdjustBalance(decimal newAmount, string reason)
    {
        if (!IsActive)
            throw new InvalidOperationException("La cuenta está inactiva");

        decimal difference = newAmount - Balance.Amount;
        // Si la diferencia es 0, no hay ajuste que hacer
        if (difference == 0) return;

        Money adjustmentValue = new(Math.Abs(difference), Balance.Currency);
        string movementDescription = $"{reason} (Ajuste)";

        _movements.Add(new Movement(adjustmentValue, movementDescription));
        Balance = new Money(newAmount, Balance.Currency);
    }
}