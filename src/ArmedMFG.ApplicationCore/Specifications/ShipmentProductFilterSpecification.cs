using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.OrderAggregate;

namespace ArmedMFG.ApplicationCore.Specifications;

public class ShipmentProductFilterSpecification : Specification<ShipmentProduct>
{
    public ShipmentProductFilterSpecification()
    {
        Query
            .Include(sp => sp.OrderShipment);
    }
}
