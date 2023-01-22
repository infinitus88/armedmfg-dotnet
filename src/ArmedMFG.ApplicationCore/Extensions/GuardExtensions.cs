using System.Collections.Generic;
using System.Linq;
using ArmedMFG.ApplicationCore.Entities.BasketAggregate;
using ArmedMFG.ApplicationCore.Exceptions;

namespace Ardalis.GuardClauses;

public static class BasketGuards
{
    public static void EmptyBasketOnCheckout(this IGuardClause guardClause, IReadOnlyCollection<BasketItem> basketItems)
    {
        if (!basketItems.Any())
            throw new EmptyBasketOnCheckoutException();
    }
}
