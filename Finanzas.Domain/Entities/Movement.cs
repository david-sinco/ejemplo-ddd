using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finanzas.Domain.ValueObjects;

namespace Finanzas.Domain.Entities;

public class Movement
{
    public Guid Id { get; private set; }
    public Money Value { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public DateTime CreatedAt { get; private set; }

    private Movement() { }

    public Movement(Money value, string description)
    {
        Id = Guid.NewGuid();
        Value = value;
        Description = description;
        CreatedAt = DateTime.UtcNow;
    }
}
