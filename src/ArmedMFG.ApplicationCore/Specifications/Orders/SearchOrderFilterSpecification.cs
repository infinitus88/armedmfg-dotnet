using System;
using System.Drawing;
using Ardalis.Specification;
using ArmedMFG.ApplicationCore.Entities.OrderAggregate;

namespace ArmedMFG.ApplicationCore.Specifications.Orders;

public class SearchOrderFilterSpecification : Specification<Order>
{
    public SearchOrderFilterSpecification(string searchText, DateTime? startDate, DateTime? endDate)
    {
        Query.Where(o => (!startDate.HasValue || o.OrderedDate <= startDate) &&
                         (!endDate.HasValue || o.OrderedDate >= endDate));
    }
}
