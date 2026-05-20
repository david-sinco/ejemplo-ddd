using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finanzas.Domain.ValueObjects;

public record Money(decimal Amount, string Currency)
{
    public Money Subtract(Money other)
    {
        if (Currency != other.Currency)
            throw new InvalidOperationException("No se pueden operar distintas monedas.");
        return new Money(Amount - other.Amount, Currency);
    }
}
