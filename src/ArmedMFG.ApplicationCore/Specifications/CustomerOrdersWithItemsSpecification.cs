using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.OrderCatalogAggregate;

namespace ArmedMFG.ApplicationCore.Specifications;

public class CustomerOrdersWithItemsSpecification : Specification<Order>
{
    public CustomerOrdersWithItemsSpecification(string buyerId)
    {
        Query.Where(o => o.BuyerId == buyerId)
            .Include(o => o.OrderItems)
                .ThenInclude(i => i.ItemOrdered);
    }
}
