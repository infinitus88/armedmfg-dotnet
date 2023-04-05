using System;
using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.OrderAggregate;

namespace ArmedMFG.ApplicationCore.Specifications;

public class OrderShipmentFilterSpecification : Specification<OrderShipment>
{
    public OrderShipmentFilterSpecification(DateTime? startDate, DateTime? endDate, string customerName, string driverName, string carNumber)
    {
        Query
            .Include(o => o.Order)
            .ThenInclude(o => o.Customer)
            .Where(o => ((!startDate.HasValue || o.ShipmentDate <= startDate) &&
                        (!endDate.HasValue || o.ShipmentDate >= endDate)) &&
                        ((!String.IsNullOrEmpty(customerName) ||
                         o.Order.Customer.FullName.ToLower().Contains(customerName.ToLower())) ||
                        (!String.IsNullOrEmpty(driverName) || o.DriverName.ToLower().Contains(driverName.ToLower())) ||
                        (!String.IsNullOrEmpty(carNumber) || o.CarNumber.ToLower().Contains(carNumber.ToLower()))));

    }

    public OrderShipmentFilterSpecification()
    {
        Query.OrderBy(o => o.ShipmentDate);
    }
}

public class ShipmentProductFilterSpecification : Specification<ShipmentProduct>
{
    public ShipmentProductFilterSpecification()
    {
        Query
            .Include(sp => sp.OrderShipment);
    }
}
