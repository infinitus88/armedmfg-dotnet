using System;
using System.Drawing;
using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.OrderAggregate;

namespace ArmedMFG.ApplicationCore.Specifications;

public class OrderFilterSpecification : Specification<Order>
{
    public OrderFilterSpecification(DateTime? startDate, DateTime? endDate, string? customerFullName)
    {
        Query.Where(o => (!startDate.HasValue || o.OrderedDate <= startDate) &&
                         (!endDate.HasValue || o.OrderedDate >= endDate) &&
                         (!String.IsNullOrWhiteSpace(customerFullName) || o.Customer.FullName.ToLower().Contains(customerFullName.ToLower())));
    }
}
