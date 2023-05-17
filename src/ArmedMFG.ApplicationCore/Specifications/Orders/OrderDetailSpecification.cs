using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.OrderAggregate;

namespace ArmedMFG.ApplicationCore.Specifications.Orders;

public sealed class OrderDetailSpecification : Specification<Order>, ISingleResultSpecification
{
    public OrderDetailSpecification(int orderId)
    {
        Query
            .Where(o => o.Id == orderId)
            .Include(o => o.Customer)
            .Include(o => o.OrderProducts)
            .ThenInclude(p => p.ProductType)
            .Include(o => o.OrderShipments)
            .ThenInclude(o => o.ShipmentProducts);
    }
}
